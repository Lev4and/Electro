using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Controllers
{
    public class ContactUsController : Controller
    {
        private readonly DataManager _dataManager;

        public ContactUsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/ContactUs")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
