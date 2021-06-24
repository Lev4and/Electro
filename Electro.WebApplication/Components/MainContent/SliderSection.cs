using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components.MainContent
{
    public class SliderSection : ViewComponent
    {
        public SliderSection()
        {

        }

        public IViewComponentResult Invoke()
        {
            return View("~/Views/Home/Components/MainContent/SliderSection/Default.cshtml");
        }
    }
}
