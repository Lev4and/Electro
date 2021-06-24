using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Header.VerticalAndSecondaryMenu
{
    public class VerticalMenu : ViewComponent
    {
        public VerticalMenu()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Header/VerticalAndSecondaryMenu/VerticalMenu/Default.cshtml");
        }
    }
}
