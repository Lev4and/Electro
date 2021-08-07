using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Electro.Model.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Parsers.DNS
{
    public class CategoryParser : IParser<Dictionary<string, Category>>
    {
        private string GetLinkInCategoryItem(IElement categoryItem)
        {
            return categoryItem.QuerySelector("a.subcategory__childs-item").GetAttribute("href");
        }

        private string GetTitleInCategoryItem(IElement categoryItem)
        {
            return categoryItem.QuerySelector("span.subcategory__title").TextContent;
        }

        private string GetSourceInCategoryItem(IElement categoryItem)
        {
            return categoryItem.QuerySelectorAll("source").FirstOrDefault(source =>
                    source.Attributes.Any(attribute => attribute.Name == "media" &&
                        attribute.Value == "(max-width: 767px)")).GetAttribute("data-srcset");
        }

        private IHtmlCollection<IElement> GetCategoryItems(IHtmlDocument document)
        {
            return document.QuerySelectorAll("div.subcategory__item");
        }

        public Dictionary<string, Category> Parse(IHtmlDocument document)
        {
            var categories = new Dictionary<string, Category>();
            var categoryItems = GetCategoryItems(document);

            foreach(var categoryItem in categoryItems)
            {
                var link = GetLinkInCategoryItem(categoryItem);
                var title = GetTitleInCategoryItem(categoryItem);
                var source = GetSourceInCategoryItem(categoryItem);

                if(title != "Услуги")
                {
                    categories.Add(link, new Category()
                    {
                        Name = title,
                        Photo = new CategoryPhoto()
                        {
                            Url = source
                        }
                    });
                }
            }

            return categories;
        }
    }
}
