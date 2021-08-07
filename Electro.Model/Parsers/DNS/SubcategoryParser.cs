using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using Electro.Model.Database.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Parsers.DNS
{
    public class SubcategoryParser : IParser<Dictionary<string, Category>>
    {
        private string GetLinkInSubcategoryItem(IElement subcategoryItem)
        {
            return subcategoryItem.GetAttribute("href");
        }

        private string GetTitleInSubcategoryItem(IElement subcategoryItem)
        {
            return subcategoryItem.QuerySelector("span.subcategory__title").TextContent;
        }

        private string GetSourceInSubcategoryItem(IElement subcategoryItem)
        {
            var source = subcategoryItem.QuerySelectorAll("source");

            if(source != null ? source.Count() > 0 : false)
            {
                return source.FirstOrDefault(source =>
                    source.Attributes.Any(attribute => attribute.Name == "media" &&
                        attribute.Value == "(max-width: 767px)")).GetAttribute("data-srcset");
            }

            return "";
        }

        private IHtmlCollection<IElement> GetSubcategoryItems(IHtmlDocument document)
        {
            return document.QuerySelectorAll("a.subcategory__item");
        }

        public Dictionary<string, Category> Parse(IHtmlDocument document)
        {
            var subcategories = new Dictionary<string, Category>();
            var subcategoryItems = GetSubcategoryItems(document);

            foreach (var subcategoryItem in subcategoryItems)
            {
                var link = GetLinkInSubcategoryItem(subcategoryItem);
                var title = GetTitleInSubcategoryItem(subcategoryItem);
                var source = GetSourceInSubcategoryItem(subcategoryItem);

                subcategories.Add(link, new Category()
                {
                    Name = title,
                    Photo = new CategoryPhoto()
                    {
                        Url = source
                    }
                });
            }

            return subcategories;
        }
    }
}
