using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Controllers
{
    public class TrackingOrder : Controller
    {
        private readonly DataManager _dataManager;

        public TrackingOrder(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/TrackingOrder")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
