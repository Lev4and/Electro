using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class AccountSidebarToggleButton : ViewComponent
    {
        public AccountSidebarToggleButton()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
