using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System.Collections.Generic;

namespace Electro.Model.Parsers.DNS
{
    public class ProductsParser : IParser<List<string>>
    {
        private string GetLinkInProductItem(IElement productItem)
        {
            return productItem.QuerySelector("a.catalog-product__name")
                .GetAttribute("href");
        }

        private IHtmlCollection<IElement> GetProductItems(IHtmlDocument document)
        {
            return document.QuerySelectorAll("div.catalog-product");
        }

        public List<string> Parse(IHtmlDocument document)
        {
            var links = new List<string>();
            var productItems = GetProductItems(document);

            foreach(var productItem in productItems)
            {
                links.Add(GetLinkInProductItem(productItem));
            }

            return links;
        }
    }
}
