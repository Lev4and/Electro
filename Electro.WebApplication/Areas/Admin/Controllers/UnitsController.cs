using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UnitsController : Controller
    {
        private readonly DataManager _dataManager;

        public UnitsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var viewModel = new UnitsViewModel()
            {
                Units = _dataManager.Units.GetUnits().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            return View(new Unit());
        }

        [Route("~/Admin/Units/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            return View(_dataManager.Units.GetUnitById(id));
        }

        [HttpPost]
        public IActionResult Save(Unit viewModel)
        {
            if (ModelState.IsValid)
            {
                if (_dataManager.Units.SaveUnit(viewModel))
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(nameof(viewModel.Name), "Единица измерения с таким названием уже существует");
                }
            }

            return View(viewModel);
        }

        [Route("~/Admin/Units/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.Units.DeleteUnitById(id);

            return RedirectToAction("Index");
        }
    }
}
