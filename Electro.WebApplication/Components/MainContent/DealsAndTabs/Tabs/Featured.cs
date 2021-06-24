using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.DealsAndTabs.Tabs
{
    public class Featured : ViewComponent
    {
        public Featured()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/DealsAndTabs/Tabs/Featured/Default.cshtml");
        }
    }
}
