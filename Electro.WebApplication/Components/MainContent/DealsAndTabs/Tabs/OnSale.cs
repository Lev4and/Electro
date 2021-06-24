using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.DealsAndTabs.Tabs
{
    public class OnSale : ViewComponent
    {
        public OnSale()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/DealsAndTabs/Tabs/OnSale/Default.cshtml");
        }
    }
}
