using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CharacteristicsController : Controller
    {
        private readonly DataManager _dataManager;

        public CharacteristicsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            var viewModel = new CharacteristicsViewModel()
            {
                Characteristics = _dataManager.Characteristics.GetCharacteristics().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            var viewModel = new CharacteristicViewModel()
            {
                Characteristic = new Characteristic(),
                SelectedQuantityUnits = new List<Guid>(),
                UsedQuantityUnits = new List<QuantityUnit>(),
                NotUsedQuantityUnitsForCharacteristic = _dataManager.QuantityUnits.GetQuantityUnits().ToList()
            };

            return View(viewModel);
        }

        [Route("~/Admin/Characteristics/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new CharacteristicViewModel()
            {
                SelectedQuantityUnits = new List<Guid>(),
                UsedQuantityUnits = new List<QuantityUnit>(),
                Characteristic = _dataManager.Characteristics.GetCharacteristicById(id),
                NotUsedQuantityUnitsForCharacteristic = _dataManager.QuantityUnits.GetNotUsedQuantityUnitsForCharacteristic(id).ToList()
            };

            if (viewModel.Characteristic.QuantityUnits != null)
            {
                foreach (var characteristicQuantityUnit in viewModel.Characteristic.QuantityUnits)
                {
                    viewModel.UsedQuantityUnits.Add(characteristicQuantityUnit.QuantityUnit);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(CharacteristicViewModel viewModel)
        {
            if (_dataManager.Characteristics.SaveCharacteristic(viewModel.Characteristic))
            {
                if (viewModel.Characteristic.QuantityUnits != null)
                {
                    var notUsedCharacteristicQuantityUnits = new List<CharacteristicQuantityUnit>();

                    foreach (var characteristicQuantityUnit in viewModel.Characteristic.QuantityUnits)
                    {
                        if (viewModel.SelectedQuantityUnits != null ? !viewModel.SelectedQuantityUnits.Contains(characteristicQuantityUnit.QuantityUnitId) : true)
                        {
                            notUsedCharacteristicQuantityUnits.Add(characteristicQuantityUnit);
                        }
                    }

                    _dataManager.CharacteristicQuantityUnits.DeleteRangeCharacteristicQuantityUnits(notUsedCharacteristicQuantityUnits);
                }

                if (viewModel.SelectedQuantityUnits != null)
                {
                    foreach (var quantityUnitId in viewModel.SelectedQuantityUnits)
                    {
                        _dataManager.CharacteristicQuantityUnits.SaveCharacteristicQuantityUnit(new CharacteristicQuantityUnit() 
                        { 
                            CharacteristicId = viewModel.Characteristic.Id, 
                            QuantityUnitId = quantityUnitId 
                        });
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Characteristic.Name", "Характеристика с таким именем уже существует");
            }

            return View(viewModel);
        }

        [Route("~/Admin/Characteristics/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.Characteristics.DeleteCharacteristicById(id);

            return RedirectToAction("Index");
        }
    }
}
