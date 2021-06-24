using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Footer.TopWidget
{
    public class FeaturedProducts : ViewComponent
    {
        public FeaturedProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Footer/TopWidget/FeaturedProducts/Default.cshtml");
        }
    }
}
