using Electro.Model.Database;
using Electro.Model.Database.AuxiliaryTypes;
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

        private CharacteristicsFilters InitCharacteristicsFilters()
        {
            var filtres = new CharacteristicsFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 10,
                SearchString = "",
                SortingOption = SortingOption.Default
            };

            return filtres;
        }

        private Pagination InitPagination(CharacteristicsFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.Characteristics.GetCountCharacteristics(filters)
            };

            return pagination;
        }

        private CharacteristicsViewModel GetCharacteristicsViewModel(CharacteristicsFilters filters = null, int? numberPage = null)
        {
            if (filters == null)
            {
                filters = InitCharacteristicsFilters();
            }

            if (numberPage != null)
            {
                filters.NumberPage = (int)numberPage;
            }

            var pagination = InitPagination(filters);

            var viewModel = new CharacteristicsViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Limits = new List<int>()
                {
                    5, 10, 15, 20, 25, 50, 100
                },
                Characteristics = _dataManager.Characteristics.GetCharacteristics(filters).ToList(),
                SortingOptions = new Dictionary<SortingOption, string>()
                {
                    { SortingOption.Default, "Сортировка по умолчанию" },
                    { SortingOption.ByAscendingName, "Сортировка по названию: от А до Я" },
                    { SortingOption.ByDescendingName, "Сортировка по названию: от Я до А" },
                }
            };

            return viewModel;
        }

        public IActionResult Index()
        {
            return View(GetCharacteristicsViewModel());
        }

        [Route("~/Admin/Characteristics/Index/{numberPage}")]
        public IActionResult Index(int numberPage)
        {
            return View(GetCharacteristicsViewModel(numberPage: numberPage));
        }

        [HttpPost]
        public IActionResult Index(CharacteristicsViewModel viewModel)
        {
            return View(GetCharacteristicsViewModel(viewModel.Filters));
        }

        public IActionResult Save()
        {
            return View(new Characteristic());
        }

        [Route("~/Admin/Characteristics/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            return View(_dataManager.Characteristics.GetCharacteristicById(id));
        }

        [HttpPost]
        public IActionResult Save(Characteristic viewModel)
        {
            if (_dataManager.Characteristics.SaveCharacteristic(viewModel))
            {
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Name", "Характеристика с таким именем уже существует");
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
