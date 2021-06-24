using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.Bestsellers
{
    public class SlickCarousel : ViewComponent
    {
        public SlickCarousel()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/Bestsellers/SlickCarousel/Default.cshtml");
        }
    }
}
