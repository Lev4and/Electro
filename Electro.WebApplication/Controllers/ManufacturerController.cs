using Electro.Model.Database;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly DataManager _dataManager;

        public ManufacturerController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        [Route("~/Manufacturer/{manufacturerId}")]
        public IActionResult Index(Guid manufacturerId)
        {
            var viewModel = new ManufacturerViewModel()
            {
                Manufacturer = _dataManager.Manufacturers.GetManufacturerById(manufacturerId),
                Categories = _dataManager.Categories.GetCategoriesByManufacturerId(manufacturerId).ToList()
            };

            return View(viewModel);
        }
    }
}
