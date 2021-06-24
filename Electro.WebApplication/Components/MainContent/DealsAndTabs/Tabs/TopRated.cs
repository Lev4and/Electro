using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.DealsAndTabs.Tabs
{
    public class TopRated : ViewComponent
    {
        public TopRated()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/DealsAndTabs/Tabs/TopRated/Default.cshtml");
        }
    }
}
