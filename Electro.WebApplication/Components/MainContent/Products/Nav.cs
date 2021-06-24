using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.Products
{
    public class Nav : ViewComponent
    {
        public Nav()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/Products/Nav/Default.cshtml");
        }
    }
}
