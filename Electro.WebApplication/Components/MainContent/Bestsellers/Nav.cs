using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent.Bestsellers
{
    public class Nav : ViewComponent
    {
        public Nav()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/Bestsellers/Nav/Default.cshtml");
        }
    }
}
