using Electro.Model.Database;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services;
using Electro.WebApplication.Services.ImageResizable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoriesController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly FileService _fileService;
        private readonly ImageService _imageService;

        public CategoriesController(DataManager dataManager, FileService fileService, ImageService imageService)
        {
            _dataManager = dataManager;
            _fileService = fileService;
            _imageService = imageService;
        }

        private CategoriesFilters InitCategoriesFilters()
        {
            var filtres = new CategoriesFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 10,
                SearchString = "",
                ParentCategoryId = null,
                SortingOption = SortingOption.Default
            };

            return filtres;
        }

        private Pagination InitPagination(CategoriesFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.Categories.GetCountCategories(filters)
            };

            return pagination;
        }

        private CategoriesViewModel GetCategoriesViewModel(CategoriesFilters filters = null, int? numberPage = null)
        {
            if(filters == null)
            {
                filters = InitCategoriesFilters();
            }

            if (numberPage != null)
            {
                filters.NumberPage = (int)numberPage;
            }

            var pagination = InitPagination(filters);
            var parentCategories = filters.ParentCategoryId != null ? new List<Category> 
            { 
                _dataManager.Categories.GetCategoryById((Guid)filters.ParentCategoryId) 
            } : null;

            var viewModel = new CategoriesViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Limits = new List<int>()
                {
                    5, 10, 15, 20, 25, 50, 100
                },
                Categories = _dataManager.Categories.GetCategories(filters).ToList(),
                ParentCategories = parentCategories,
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
            return View(GetCategoriesViewModel());
        }

        [Route("~/Admin/Categories/Index/{numberPage}")]
        public IActionResult Index(int numberPage)
        {
            return View(GetCategoriesViewModel(numberPage: numberPage));
        }

        [HttpPost]
        public IActionResult Index(CategoriesViewModel viewModel)
        {
            return View(GetCategoriesViewModel(viewModel.Filters));
        }

        [HttpPost]
        public JsonResult ParentCategories(string searchString)
        {
            var parentCategories = _dataManager.Categories
                .GetCategories(searchString, 5)
                .ToList()
                .Select(category => new
                {
                    Id = category.Id,
                    Text = category.Name
                });

            return new JsonResult(new { results = parentCategories });
        }

        public IActionResult Save()
        {
            var viewModel = new CategoryViewModel()
            {
                Category = new Category() 
                { 
                    Photo = new CategoryPhoto() 
                },
                Categories = new List<Category>()
            };

            return View(viewModel);
        }

        [Route("~/Admin/Categories/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new CategoryViewModel()
            {
                Category = _dataManager.Categories.GetCategoryById(id),
                Categories = _dataManager.Categories.GetCategoriesWithoutASpecific(id).ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(CategoryViewModel viewModel, IFormFile uploadedFile)
        {
            if (_dataManager.Categories.SaveCategory(viewModel.Category))
            {
                if (uploadedFile != null)
                {
                    viewModel.Category.Photo.CategoryId = viewModel.Category.Id;
                    viewModel.Category.Photo.Url = _imageService.SaveImage(uploadedFile, ImageType.Box, $"categories/{viewModel.Category.Id}");

                    _dataManager.CategoryPhotos.SaveCategoryPhoto(viewModel.Category.Photo);
                }
                else
                {
                    if (viewModel.Category.Photo.Id != default ? string.IsNullOrEmpty(viewModel.Category.Photo.Url) : false)
                    {
                        _dataManager.CategoryPhotos.DeleteCategoryPhotoById(viewModel.Category.Photo.Id);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Category.Name", "Категория продукции с таким названием в составе родительской категории уже существует");
                ModelState.AddModelError("Category.ParentId", "Категория продукции с таким названием в составе родительской категории уже существует");
            }

            return View(viewModel);
        }

        [Route("~/Admin/Categories/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _fileService.DeleteDirectory($"upload/categories/{id}");
            _dataManager.Categories.DeleteCategoryById(id);

            return RedirectToAction("Index");
        }
    }
}
