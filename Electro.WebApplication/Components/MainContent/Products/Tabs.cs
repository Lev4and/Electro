using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.Products
{
    public class Tabs : ViewComponent
    {
        public Tabs()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/Products/Tabs/Default.cshtml");
        }
    }
}
