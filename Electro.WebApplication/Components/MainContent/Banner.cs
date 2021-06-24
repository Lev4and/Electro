using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent
{
    public class Banner : ViewComponent
    {
        public Banner()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/Banner/Default.cshtml");
        }
    }
}
