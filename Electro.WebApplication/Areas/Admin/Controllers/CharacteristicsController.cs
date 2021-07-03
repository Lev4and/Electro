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

        [HttpGet]
        public IActionResult SaveCharacteristicCategory(Guid characteristicCategoryId)
        {
            var viewModel = new CharacteristicCategoryViewModel()
            {
                SectionsCharacteristics = new List<SectionCharacteristic>(),
                Categories = _dataManager.Categories.GetCategories().ToList(),
                CharacteristicCategory = _dataManager.CharacteristicCategories
                    .GetCharacteristicCategoryById(characteristicCategoryId)
            };

            return PartialView("_FormCharacteristicCategoryPartial", viewModel);
        }

        [HttpPost]
        public IActionResult SaveCharacteristicCategory(CharacteristicCategoryViewModel viewModel)
        {
            if ((viewModel.CharacteristicCategory.CategoryId != null ? viewModel.CharacteristicCategory.CategoryId != default : false) 
                 && (viewModel.CharacteristicCategory.SectionId != null ? viewModel.CharacteristicCategory.SectionId != default : false))
            {
                if (_dataManager.CharacteristicCategories.SaveCharacteristicCategory(viewModel.CharacteristicCategory))
                {
                    viewModel.Categories = _dataManager.Categories.GetCategories().ToList();
                    viewModel.SectionsCharacteristics = new List<SectionCharacteristic>();
                    viewModel.CharacteristicCategory = new CharacteristicCategory();
                }
                else
                {
                    ModelState.AddModelError("CharacteristicCategory.CategoryId", "Характеристика для категории товара с такими данными уже существует");
                    ModelState.AddModelError("CharacteristicCategory.SectionId", "Характеристика для категории товара с такими данными уже существует");
                }
            }
            else
            {
                if (viewModel.CharacteristicCategory.CategoryId == null ? true : viewModel.CharacteristicCategory.CategoryId == default)
                {
                    ModelState.AddModelError("CharacteristicCategory.CategoryId", "Категория товара должна быть указана");
                }

                if (viewModel.CharacteristicCategory.SectionId == null ? true : viewModel.CharacteristicCategory.SectionId == default)
                {
                    ModelState.AddModelError("CharacteristicCategory.SectionId", "Раздел характеристики должен быть указан");
                }
            }

            return PartialView("_FormCharacteristicCategoryPartial", viewModel);
        }

        [HttpGet]
        public IActionResult DeleteCharacteristicCategory(Guid characteristicCategoryId)
        {
            _dataManager.CharacteristicCategories.DeleteCharacteristicCategoryById(characteristicCategoryId);

            return Ok();
        }

        [HttpPost]
        public IActionResult SectionsCharacteristicByCategory(Guid categoryId)
        {
            var viewModel = new SectionsCharacteristicsViewModel()
            {
                SectionCharacteristics = _dataManager.SectionsCharacteristics
                .GetSectionsCharacteristicsByCategoryId(categoryId).ToList()
            };

            return PartialView("_SectionCharacteristicItemsPartial", viewModel);
        }

        [HttpPost]
        public IActionResult CharacteristicCategories(Guid characteristicId)
        {
            var viewModel = new CharacteristicCategoriesViewModel()
            {
                CharacteristicCategories = _dataManager.CharacteristicCategories
                    .GetCharacteristicCategoriesByCharacteristicId(characteristicId).ToList()
            };

            return PartialView("_DataTableCharacteristicCategoriesPartial", viewModel);
        }

        [Route("~/Admin/Characteristics/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _dataManager.Characteristics.DeleteCharacteristicById(id);

            return RedirectToAction("Index");
        }
    }
}
