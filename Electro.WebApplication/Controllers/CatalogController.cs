using Electro.Model.Database;
using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class CatalogController : Controller
    {
        private readonly DataManager _dataManager;

        public CatalogController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private CatalogProductsFilters InitFilters(List<ProductsManufacturer> manufacturers, List<CharacteristicCategory> characteristicCategories)
        {
            var filters = new CatalogProductsFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 20,
                RangePrice = new Price() { },
                SortingOption = SortingOption.ByAscendingPrice,
                ManufacturerFilters = manufacturers.Select(manufacturer => new ManufacturerFilter
                {
                    IsUsed = false,
                    ManufacturerId = manufacturer.ManufacturerId
                }).ToList(),
                CharacteristicFilters = characteristicCategories.Select(characteristic => new CharacteristicFilter
                {
                    CharacteristicId = characteristic.CharacteristicId,
                    CharacteristicValueFilters = characteristic.Values.Select(characteristicValue => new CharacteristicValueFilter 
                    {
                        IsUsed = false,
                        CharacteristicValueId = characteristicValue.Id
                    }).ToList()
                }).ToList()
            };

            return filters;
        }

        private Pagination InitPagination(Guid categoryId, CatalogProductsFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.Products.GetCountProductsByCategoryId(categoryId, filters)
            };

            return pagination;
        }

        private CatalogProductsViewModel GetCatalogProductsViewModel(Guid categoryId, CatalogProductsFilters filters = null, Guid? manufacturerId = null, int? numberPage = null)
        {
            var manufacturers = _dataManager.Manufacturers.GetManufacturersByCategoryId(categoryId).ToList();
            var characteristicCategories = _dataManager.CharacteristicCategories.GetCharacteristicCategoriesByCategoryId(categoryId).ToList();

            if(filters == null)
            {
                filters = InitFilters(manufacturers, characteristicCategories);
                filters.RangePrice = new Price(_dataManager.Products.GetMinPriceProductByCategoryId(categoryId),
                        _dataManager.Products.GetMaxPriceProductByCategoryId(categoryId));
            }

            if(manufacturerId != null)
            {
                filters.ManufacturerFilters.FirstOrDefault(filter =>
                    filter.ManufacturerId == manufacturerId).IsUsed = true;
            }

            if(numberPage != null)
            {
                filters.NumberPage = (int)numberPage;
            }

            var pagination = InitPagination(categoryId, filters);

            var viewModel = new CatalogProductsViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                ProductsManufacturers = manufacturers,
                CharacteristicCategories = characteristicCategories,
                Limits = new List<int>()
                {
                    5, 10, 15, 20, 40, 60, 80, 100
                },
                Category = _dataManager.Categories.GetCategoryById(categoryId),
                Products = _dataManager.Products.GetProductsByCategoryId(categoryId, filters).ToList(),
                LatestProducts = _dataManager.Products.GetLatestProductsByCategoryId(categoryId, 5).ToList(),
                SortingOptions = new Dictionary<SortingOption, string>() 
                {
                    { SortingOption.Default, "Сортировка по умолчанию" },
                    { SortingOption.ByPopularity, "Сортировка по популярности" },
                    { SortingOption.ByAverageRating, "Сортировка по среднему рейтингу" },
                    { SortingOption.ByAncient, "Сортировка по старым" },
                    { SortingOption.ByRecently, "Сортировка по последним" },
                    { SortingOption.ByAscendingPrice, "Сортировка по цене: от низкой до высокой" },
                    { SortingOption.ByDescendingPrice, "Сортировка по цене: от высокой до низкой" },
                    { SortingOption.ByAscendingName, "Сортировка по названию: от А до Я" },
                    { SortingOption.ByDescendingName, "Сортировка по названию: от Я до А" },
                }
            };

            return viewModel;
        }

        [Route("~/Catalog")]
        public IActionResult Index()
        {
            return View(_dataManager.Categories.GetParentCategories().ToList());
        }

        [Route("~/Catalog/{categoryId}")]
        public IActionResult Category(Guid categoryId)
        {
            var category = _dataManager.Categories.GetCategoryById(categoryId);

            if (category.Children != null ? category.Children.Count > 0 : false)
            {
                return View(category);
            }

            return RedirectToAction("Products", categoryId);
        }

        [Route("~/Catalog/{categoryId}/Products")]
        public IActionResult Products(Guid categoryId)
        {
            return View(GetCatalogProductsViewModel(categoryId));
        }

        [Route("~/Catalog/{categoryId}/Products/manufacturer={manufacturerId}")]
        public IActionResult Products(Guid categoryId, Guid manufacturerId)
        {
            return View(GetCatalogProductsViewModel(categoryId: categoryId, manufacturerId: manufacturerId));
        }

        [HttpPost]
        [Route("~/Catalog/{categoryId}/Products")]
        public IActionResult Products(Guid categoryId, CatalogProductsViewModel viewModel)
        {
            return View(GetCatalogProductsViewModel(categoryId, viewModel.Filters));
        }

        [Route("~/Catalog/{categoryId}/Products/{numberPage}")]
        public IActionResult Products(Guid categoryId, int numberPage)
        {
            return View(GetCatalogProductsViewModel(categoryId, numberPage: numberPage));
        }
    }
}
