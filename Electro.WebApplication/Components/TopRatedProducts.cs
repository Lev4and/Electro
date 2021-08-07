using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class TopRatedProducts : ViewComponent
    {
        public TopRatedProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
