using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent
{
    public class FullBanner : ViewComponent
    {
        public FullBanner()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/FullBanner/Default.cshtml");
        }
    }
}
