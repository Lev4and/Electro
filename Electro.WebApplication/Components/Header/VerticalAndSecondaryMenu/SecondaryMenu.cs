using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Header.VerticalAndSecondaryMenu
{
    public class SecondaryMenu : ViewComponent
    {
        public SecondaryMenu()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Header/VerticalAndSecondaryMenu/SecondaryMenu/Default.cshtml");
        }
    }
}
