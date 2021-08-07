using AngleSharp.Html.Dom;
using System.Linq;

namespace Electro.Model.Parsers.DNS
{
    public class CSRFTokenParser : IParser<string>
    {
        private string GetToken(IHtmlDocument document)
        {
            var meta = document.QuerySelectorAll("meta").FirstOrDefault(element =>
                (element.HasAttribute("name") ? element.GetAttribute("name") == "csrf-token" : false));

            if (meta != null)
            {
                return meta.GetAttribute("content");
            }

            return "";
        }

        public string Parse(IHtmlDocument document)
        {
            return GetToken(document);
        }
    }
}
