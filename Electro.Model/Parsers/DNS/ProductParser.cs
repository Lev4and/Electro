using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Electro.Model.Database.Entities;
using Electro.Model.Parsers.DNS.HtmlLoaders;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Electro.Model.Parsers.DNS
{
    public class ProductParser : IParser<Product>
    {
        private dynamic _imagesProduct;
        private readonly ProductHtmlLoader _htmlLoader;

        public ProductParser()
        {
            _imagesProduct = null;
            _htmlLoader = new ProductHtmlLoader();
        }

        private double GetPriceProduct(IHtmlDocument document)
        {
            var script = document.QuerySelectorAll("script").FirstOrDefault(element =>
                element.TextContent.Contains("sendProductMessage")).TextContent;

            var textJson = script.Substring(script.IndexOf('{'), script.LastIndexOf('}') - script.IndexOf('{') + 1);
            var productMessage = JsonConvert.DeserializeObject<dynamic>(textJson);

            return productMessage.price;
        }

        private string GetProductId(IHtmlDocument document)
        {
            var container = document.QuerySelectorAll("div.container");

            if(container != null)
            {
                return container.FirstOrDefault(element => (element.ClassList != null ? 
                    element.ClassList.Contains("product-card") : false))
                        .GetAttribute("data-product-card");
            }

            return "";
        }

        private string GetContainerId(IHtmlDocument document)
        {
            var productCardTopImages = document.QuerySelector("div.product-card-top__images");

            if(productCardTopImages != null)
            {
                var span = productCardTopImages.QuerySelector("span");

                if (span != null)
                {
                    return span.GetAttribute("id");
                }
            }

            return "";
        }

        private string GetModelProduct(IHtmlDocument document)
        {
            return document.QuerySelector("h1.product-card-top__title").TextContent;
        }

        private string GetParentCategoryNameProduct(IHtmlDocument document)
        {
            var breadcrumbItems = document.QuerySelector("ol.breadcrumb-list")
                .QuerySelectorAll("li").Where(element =>
                    element.ClassList != null ? element.ClassList.Contains("breadcrumb-list__item") &&
                        element.ClassList.Contains("initial-breadcrumb") &&
                            !element.ClassList.Contains("initial-breadcrumb_manufacturer") : false);

            if(breadcrumbItems.Count() > 2)
            {
                return breadcrumbItems.ElementAt(breadcrumbItems.Count() - 2).QuerySelector("span").TextContent;
            }

            return "";
        }

        private string GetCategoryNameProduct(IHtmlDocument document)
        {
            return document.QuerySelector("ol.breadcrumb-list")
                .QuerySelectorAll("li").Where(element => 
                    element.ClassList != null ? element.ClassList.Contains("breadcrumb-list__item") && 
                        element.ClassList.Contains("initial-breadcrumb") && 
                            !element.ClassList.Contains("initial-breadcrumb_manufacturer") : false).LastOrDefault()
                                .QuerySelector("span").TextContent;
        }

        private string GetManufacturerNameProduct(IHtmlDocument document)
        {
            var brandImage = document.QuerySelector("img.product-card-top__brand-image");

            if(brandImage != null)
            {
                return brandImage.GetAttribute("alt");
            }
            else
            {
                var breadcrumbItemManufacturer = document.QuerySelector("ol.breadcrumb-list")
                    .QuerySelectorAll("li").Where(element =>
                        element.ClassList != null ? element.ClassList.Contains("breadcrumb-list__item") &&
                            element.ClassList.Contains("initial-breadcrumb") &&
                                element.ClassList.Contains("initial-breadcrumb_manufacturer") : false).FirstOrDefault();

                if(breadcrumbItemManufacturer != null)
                {
                    return breadcrumbItemManufacturer.QuerySelector("span").TextContent;
                }
            }

            return "";
        }

        private string GetManufacturerLogoUrlProduct(IHtmlDocument document)
        {
            var brandImage = document.QuerySelector("img.product-card-top__brand-image");

            if(brandImage != null)
            {
                return brandImage.GetAttribute("data-src");
            }

            return "";
        }

        private string GetMainPhotoProduct(IHtmlDocument document)
        {
            var meta = document.QuerySelectorAll("meta").FirstOrDefault(element =>
                element.HasAttribute("property") ? element.GetAttribute("property") == "og:image" : false);

            if(meta != null)
            {
                return meta.GetAttribute("content");
            }

            return "";
        }

        private string GetProductInformationDescriptionProduct(IHtmlDocument document)
        {
            var script = document.QuerySelectorAll("script").FirstOrDefault(element =>
                element.TextContent.Contains("sendProductMessage")).TextContent;

            var textJson = script.Substring(script.IndexOf('{'), script.LastIndexOf('}') - script.IndexOf('{') + 1);
            var productMessage = JsonConvert.DeserializeObject<dynamic>(textJson);

            return productMessage.description;
        }

        private string GetCharacteristicValue(IElement element)
        {
            if(element.QuerySelector("div").Children.Count() == 0)
            {
                if(element.QuerySelector("div").TextContent.Length > 1)
                {
                    return element.QuerySelector("div").TextContent.Substring(1);
                }
                else
                {
                    return element.QuerySelector("div").TextContent;
                }
            }
            else
            {
                var result = "";

                foreach(var children in element.QuerySelector("div").Children)
                {
                    result += result.Length == 0 ? $"{children.TextContent}" : $", {children.TextContent}";
                }

                return result;
            }
        }

        public List<ProductPhoto> GetPhotosProduct(IHtmlDocument document)
        {
            var photos = new List<ProductPhoto>();

            if(_imagesProduct != null ? _imagesProduct.images != null : false)
            {
                foreach (var image in _imagesProduct.images)
                {
                    photos.Add(new ProductPhoto() { Url = image.desktop });
                }
            }

            return photos;
        }

        private List<ProductCharacteristicCategoryValue> GetProductCharacteristicValues(IHtmlDocument document)
        {
            var productCharacteristicValues = new List<ProductCharacteristicCategoryValue>();
            var productCharacteristicsContainer = document.QuerySelectorAll("div.product-characteristics").FirstOrDefault(element =>
                (element.ClassList != null ? element.ClassList.Contains("options-group") : false));

            if(productCharacteristicsContainer != null)
            {
                var rows = productCharacteristicsContainer.QuerySelectorAll("tr");

                for (int i = 0; i < rows.Count(); i++)
                {
                    if (rows[i].Children.Count() == 1)
                    {
                        var j = i + 1;

                        while (j < rows.Count())
                        {
                            if (rows[j].Children.Count() != 1)
                            {
                                productCharacteristicValues.Add(new ProductCharacteristicCategoryValue()
                                {
                                    CharacteristicCategoryValue = new CharacteristicCategoryValue()
                                    {
                                        CharacteristicCategory = new CharacteristicCategory()
                                        {
                                            Section = new SectionCharacteristic()
                                            {
                                                Name = rows[i].QuerySelector("td.table-part").TextContent
                                            },
                                            Characteristic = new Characteristic()
                                            {
                                                Name = rows[j].Children[0].QuerySelector("span").TextContent.Substring(1,
                                                    rows[j].Children[0].QuerySelector("span").TextContent.Length - 2),
                                            }
                                        },
                                        Value = GetCharacteristicValue(rows[j].Children[1])
                                    }
                                });
                            }
                            else
                            {
                                break;
                            }

                            j++;
                        }
                    }
                }
            }

            return productCharacteristicValues;
        }

        private void LoadImagesProduct(IHtmlDocument document)
        {
            var productId = GetProductId(document);
            var containerId = GetContainerId(document);

            if(!string.IsNullOrEmpty(productId) && !string.IsNullOrEmpty(containerId))
            {
                var resultJson = "";
                var resultSteam = _htmlLoader.GetProductImagesSlider(containerId, productId);

                using (StreamReader reader = new StreamReader(resultSteam.Result, Encoding.UTF8))
                {
                    resultJson = reader.ReadToEnd();
                }

                var script = resultJson.Substring(resultJson.IndexOf("window.initProductImagesSlider"),
                    resultJson.LastIndexOf(';') - resultJson.IndexOf("window.initProductImagesSlider") + 1);

                var scriptJson = script.Substring(script.IndexOf('{'), script.LastIndexOf('}') - script.IndexOf('{') + 1);

                _imagesProduct = JsonConvert.DeserializeObject<dynamic>(scriptJson.Replace(@"\\\", "").Replace(@"\\", "")
                    .Replace(@"\", ""));
            }
        }

        public Product Parse(IHtmlDocument document)
        {
            LoadImagesProduct(document);

            var product = new Product()
            {
                Model = GetModelProduct(document),
                Price = GetPriceProduct(document),
                Category = new Category()
                {
                    Name = GetCategoryNameProduct(document),
                    Parent = new Category()
                    {
                        Name = GetParentCategoryNameProduct(document)
                    }
                },
                Manufacturer = new Manufacturer()
                {
                    Name = GetManufacturerNameProduct(document),
                    Logo = new ManufacturerLogo()
                    {
                        Url = GetManufacturerLogoUrlProduct(document)
                    }
                },
                MainPhoto = new ProductMainPhoto()
                {
                    Url = GetMainPhotoProduct(document)
                },
                Information = new ProductInformation()
                {
                    Description = GetProductInformationDescriptionProduct(document)
                },
                Photos = GetPhotosProduct(document),
                CharacteristicsValues = GetProductCharacteristicValues(document)
            };

            return product;
        }
    }
}
