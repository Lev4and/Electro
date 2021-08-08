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
    public class SectionsCharacteristicsController : Controller
    {
        private readonly DataManager _dataManager;

        public SectionsCharacteristicsController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private SectionsCharacteristicsFilters InitSectionsCharacteristicsFilters()
        {
            var filtres = new SectionsCharacteristicsFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 10,
                SearchString = "",
                SortingOption = SortingOption.Default
            };

            return filtres;
        }

        private Pagination InitPagination(SectionsCharacteristicsFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.SectionsCharacteristics.GetCountSectionsCharacteristics(filters)
            };

            return pagination;
        }

        private SectionsCharacteristicsViewModel GetSectionsCharacteristicsViewModel(SectionsCharacteristicsFilters filters = null, int? numberPage = null)
        {
            if (filters == null)
            {
                filters = InitSectionsCharacteristicsFilters();
            }

            if (numberPage != null)
            {
                filters.NumberPage = (int)numberPage;
            }

            var pagination = InitPagination(filters);

            var viewModel = new SectionsCharacteristicsViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Limits = new List<int>()
                {
                    5, 10, 15, 20, 25, 50, 100
                },
                SectionCharacteristics = _dataManager.SectionsCharacteristics.GetSectionsCharacteristics(filters).ToList(),
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
            return View(GetSectionsCharacteristicsViewModel());
        }

        [Route("~/Admin/SectionsCharacteristics/Index/{numberPage}")]
        public IActionResult Index(int numberPage)
        {
            return View(GetSectionsCharacteristicsViewModel(numberPage: numberPage));
        }

        [HttpPost]
        public IActionResult Index(SectionsCharacteristicsViewModel viewModel)
        {
            return View(GetSectionsCharacteristicsViewModel(viewModel.Filters));
        }

        public IActionResult Save()
        {
            var viewModel = new SectionCharacteristicViewModel()
            {
                UsedCategories = new List<Category>(),
                SelectedCategories = new List<Guid>(),
                SectionCharacteristic = new SectionCharacteristic(),
                NotUsedCategoriesForSectionCharacteristic = _dataManager.Categories.GetCategories().ToList()
            };

            return View(viewModel);
        }

        [Route("~/Admin/SectionsCharacteristics/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new SectionCharacteristicViewModel()
            {
                UsedCategories = new List<Category>(),
                SelectedCategories = new List<Guid>(),
                SectionCharacteristic = _dataManager.SectionsCharacteristics.GetSectionCharacteristicById(id),
                NotUsedCategoriesForSectionCharacteristic = _dataManager.Categories
                    .GetNotUsedCategoriesForSectionCharacteristic(id).ToList()
            };

            if (viewModel.SectionCharacteristic.Categories != null)
            {
                foreach (var category in viewModel.SectionCharacteristic.Categories)
                {
                    viewModel.UsedCategories.Add(category.Category);
                }
            }

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(SectionCharacteristicViewModel viewModel)
        {
            if (_dataManager.SectionsCharacteristics.SaveSectionCharacteristic(viewModel.SectionCharacteristic))
            {
                if (viewModel.SectionCharacteristic.Categories != null)
                {
                    var notUsedSectionCharacteristicCategories = new List<SectionCharacteristicCategory>();

                    foreach (var category in viewModel.SectionCharacteristic.Categories)
                    {
                        if (viewModel.SelectedCategories != null ? 
                                !viewModel.SelectedCategories.Contains(category.CategoryId) : true)
                        {
                            notUsedSectionCharacteristicCategories.Add(category);
                        }
                    }

                    _dataManager.SectionCharacteristicCategories
                        .DeleteRangeSectionCharacteristicCategories(notUsedSectionCharacteristicCategories);
                }

                if (viewModel.SelectedCategories != null)
                {
                    foreach (var categoryId in viewModel.SelectedCategories)
                    {
                        _dataManager.SectionCharacteristicCategories.SaveSectionCharacteristicCategory(
                            new SectionCharacteristicCategory() 
                            { 
                                SectionId = viewModel.SectionCharacteristic.Id, 
                                CategoryId = categoryId 
                            });
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("SectionCharacteristic.Name", "Раздел характеристики с таким именем уже существует");
            }

            return View(viewModel);
        }

        public IActionResult Delete(Guid id)
        {
            _dataManager.SectionsCharacteristics.DeleteSectionCharacteristicById(id);

            return RedirectToAction("Index");
        }
    }
}
