using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Footer.TopWidget
{
    public class TopRatedProducts : ViewComponent
    {
        public TopRatedProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Footer/TopWidget/TopRatedProducts/Default.cshtml");
        }
    }
}
