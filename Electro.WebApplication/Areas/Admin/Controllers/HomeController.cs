using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public HomeController()
        {

        }

        [Route("~/Admin")]
        [Route("~/Admin/Home")]
        [Route("~/Admin/Home/Index")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
