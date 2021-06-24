using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Footer.TopWidget
{
    public class OnSaleProducts : ViewComponent
    {
        public OnSaleProducts()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Footer/TopWidget/OnSaleProducts/Default.cshtml");
        }
    }
}
