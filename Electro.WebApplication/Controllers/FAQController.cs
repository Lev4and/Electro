using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Controllers
{
    public class FAQController : Controller
    {
        [Route("~/FAQ")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
