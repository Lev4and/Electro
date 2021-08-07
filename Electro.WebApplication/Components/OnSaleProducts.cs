using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class OnSaleProducts : ViewComponent
    {
        public OnSaleProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
