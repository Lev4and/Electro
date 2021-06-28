using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuantitiesController : Controller
    {
        private readonly DataManager _dataManager;

        public QuantitiesController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var viewModel = new QuantitiesViewModel()
            {
                Quantities = _dataManager.Quantities.GetQuantities().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            var viewModel = new QuantityViewModel()
            {
                Quantity = new Quantity(),
                UsedUnits = new List<Unit>(),
                SelectedUnits = new List<Guid>(),
                NotUsedUnitsForQuantity = _dataManager.Units.GetUnits().ToList()
            };

            return View(viewModel);
        }

        [Route("~/Admin/Quantities/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new QuantityViewModel()
            {
                UsedUnits = new List<Unit>(),
                SelectedUnits = new List<Guid>(),
                Quantity = _dataManager.Quantities.GetQuantityById(id),
                NotUsedUnitsForQuantity = _dataManager.Units.GetnNotUsedUnitsForQuantityByQuantityId(id).ToList()
            };

            if(viewModel.Quantity.QuantityUnits != null)
            {
                foreach(var quantityUnit in viewModel.Quantity.QuantityUnits)
                {
                    viewModel.UsedUnits.Add(quantityUnit.Unit);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(QuantityViewModel viewModel)
        {
            if (_dataManager.Quantities.SaveQuantity(viewModel.Quantity))
            {
                if(viewModel.Quantity.QuantityUnits != null)
                {
                    var notUsedQuantityUnits = new List<QuantityUnit>();

                    foreach (var quantityUnit in viewModel.Quantity.QuantityUnits)
                    {
                        if (viewModel.SelectedUnits != null ? !viewModel.SelectedUnits.Contains(quantityUnit.UnitId) : true)
                        {
                            notUsedQuantityUnits.Add(quantityUnit);
                        }
                    }

                    _dataManager.QuantityUnits.DeleteRangeQuantityUnits(notUsedQuantityUnits);
                }

                if (viewModel.SelectedUnits != null)
                {
                    foreach(var unitId in viewModel.SelectedUnits)
                    {
                        _dataManager.QuantityUnits.SaveQuantityUnit(new QuantityUnit() { QuantityId = viewModel.Quantity.Id, UnitId = unitId });
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Quantity.Name", "Величина с таким именем уже существует");
            }

            return View(viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            _dataManager.Quantities.DeleteQuantityById(id);

            return RedirectToAction("Index");
        }
    }
}
