using AngleSharp.Html.Dom;

namespace Electro.Model.Parsers
{
    public interface IParser<T> where T : class
    {
        T Parse(IHtmlDocument document);
    }
}
