using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using System;
using System.Linq;

namespace Electro.Model.Parsers.DNS
{
    public class PaginationParser : IParser<Pagination>
    {
        private int GetLastNumberPageInPaginationWidgetPages(IElement paginationWidgetPages)
        {
            if(paginationWidgetPages != null)
            {
                return Convert.ToInt32(paginationWidgetPages.LastElementChild
                    .GetAttribute("data-page-number"));
            }

            return 1;
        }

        private IElement GetPaginationWidgetPages(IHtmlDocument document)
        {
            return document.QuerySelector("ul.pagination-widget__pages");
        }

        public Pagination Parse(IHtmlDocument document)
        {
            return new Pagination() 
            {
                NumberPage = 1,
                CountPages = GetLastNumberPageInPaginationWidgetPages(GetPaginationWidgetPages(document))
            };
        }
    }
}
