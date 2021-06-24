using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent
{
    public class RecentlyViewed : ViewComponent
    {
        public RecentlyViewed()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/RecentlyViewed/Default.cshtml");
        }
    }
}
