using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.Header.Topbar
{
    public class AccountSidebarToggleButton : ViewComponent
    {
        public AccountSidebarToggleButton()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Shared/Components/Header/Topbar/AccountSidebarToggleButton/Default.cshtml");
        }
    }
}
