using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Controllers
{
    public class TermsAndConditions : Controller
    {
        [Route("~/TermsAndConditions")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
