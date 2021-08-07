using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class FeaturedProducts : ViewComponent
    {
        public FeaturedProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
