using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class Language : ViewComponent
    {
        public Language()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("Default");
        }
    }
}
