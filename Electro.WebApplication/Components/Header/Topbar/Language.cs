using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Header.Topbar
{
    public class Language : ViewComponent
    {
        public Language()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Header/Topbar/Language/Default.cshtml");
        }
    }
}
