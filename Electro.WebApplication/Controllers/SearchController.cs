using Electro.Model.Database;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataManager _dataManager;

        public SearchController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private Pagination InitPagination(ProductsFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.Products.GetCountProducts(filters)
            };

            return pagination;
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

        private Pagination InitPagination(ManufacturersFilters filters)
        {
            var pagination = new Pagination()
            {
                NumberPage = filters.NumberPage,
                ItemsPerPage = filters.ItemsPerPage,
                CountTotalItems = _dataManager.Manufacturers.GetCountManufacturers(filters)
            };

            return pagination;
        }

        private ProductsFilters InitProductsFilters(string searchString)
        {
            var filtres = new ProductsFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 15,
                SearchString = searchString,
                SortingOption = SortingOption.ByAscendingName
            };

            return filtres;
        }

        private CategoriesFilters InitCategoriesFilters(string searchString)
        {
            var filtres = new CategoriesFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 18,
                SearchString = searchString,
                SortingOption = SortingOption.ByAscendingName
            };

            return filtres;
        }

        private ManufacturersFilters InitManufacturersFilters(string searchString)
        {
            var filtres = new ManufacturersFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 18,
                SearchString = searchString,
                SortingOption = SortingOption.ByAscendingName
            };

            return filtres;
        }

        private ProductsViewModel GetProductsViewModel(string searchString, ProductsFilters filters = null)
        {
            if (filters == null)
            {
                filters = InitProductsFilters(searchString);
            }

            var pagination = InitPagination(filters);

            var viewModel = new ProductsViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Products = _dataManager.Products.GetProducts(filters, isLiteVersion: false).ToList()
            };

            return viewModel;
        }

        private CategoriesViewModel GetCategoriesViewModel(string searchString, CategoriesFilters filters = null)
        {
            if (filters == null)
            {
                filters = InitCategoriesFilters(searchString);
            }

            var pagination = InitPagination(filters);

            var viewModel = new CategoriesViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Categories = _dataManager.Categories.GetCategories(filters).ToList()
            };

            return viewModel;
        }

        private ManufacturersViewModel GetManufacturersViewModel(string searchString, ManufacturersFilters filters = null)
        {
            if (filters == null)
            {
                filters = InitManufacturersFilters(searchString);
            }

            var pagination = InitPagination(filters);

            var viewModel = new ManufacturersViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Manufacturers = _dataManager.Manufacturers.GetManufacturers(filters).ToList()
            };

            return viewModel;
        }

        private SearchViewModel GetSearchViewModel(string searchString, ProductsFilters productsFilters = null, CategoriesFilters categoriesFilters = null, ManufacturersFilters manufacturersFilters = null)
        {
            var viewModel = new SearchViewModel()
            {
                SearchString = searchString,
                ProductsViewModel = GetProductsViewModel(searchString, productsFilters),
                CategoriesViewModel = GetCategoriesViewModel(searchString, categoriesFilters),
                ManufacturersViewModel = GetManufacturersViewModel(searchString, manufacturersFilters)
            };

            return viewModel;
        }

        public IActionResult Index(string searchString)
        {
            return View(GetSearchViewModel(searchString));
        }

        [HttpPost]
        public IActionResult Index(SearchViewModel viewModel)
        {
            return View(GetSearchViewModel(viewModel.SearchString, viewModel.ProductsViewModel.Filters, 
                viewModel.CategoriesViewModel.Filters, viewModel.ManufacturersViewModel.Filters));
        }
    }
}
