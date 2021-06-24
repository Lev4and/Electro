using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.DealsAndTabs.Deal
{
    public class Deal : ViewComponent
    {
        public Deal()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/DealsAndTabs/Deal/Default.cshtml");
        }
    }
}
