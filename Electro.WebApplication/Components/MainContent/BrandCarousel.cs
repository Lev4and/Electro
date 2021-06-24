using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent
{
    public class BrandCarousel : ViewComponent
    {
        public BrandCarousel()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/BrandCarousel/Default.cshtml");
        }
    }
}
