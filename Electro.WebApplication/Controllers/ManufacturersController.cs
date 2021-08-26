using Electro.Model.Database;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Controllers
{
    public class ManufacturersController : Controller
    {
        private readonly DataManager _dataManager;

        public ManufacturersController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        private ManufacturersFilters InitManufacturersFilters()
        {
            var filtres = new ManufacturersFilters()
            {
                NumberPage = 1,
                ItemsPerPage = 36,
                SearchString = "",
                SortingOption = SortingOption.ByAscendingName
            };

            return filtres;
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

        private ManufacturersViewModel GetManufacturersViewModel(ManufacturersFilters filters = null, int? numberPage = null)
        {
            if (filters == null)
            {
                filters = InitManufacturersFilters();
            }

            if (numberPage != null)
            {
                filters.NumberPage = (int)numberPage;
            }

            var pagination = InitPagination(filters);

            var viewModel = new ManufacturersViewModel()
            {
                Filters = filters,
                Pagination = pagination,
                Limits = new List<int>()
                {
                    5, 10, 15, 20, 25, 50, 100
                },
                Manufacturers = _dataManager.Manufacturers.GetManufacturers(filters).ToList(),
                SortingOptions = new Dictionary<SortingOption, string>()
                {
                    { SortingOption.Default, "Сортировка по умолчанию" },
                    { SortingOption.ByAscendingName, "Сортировка по названию: от А до Я" },
                    { SortingOption.ByDescendingName, "Сортировка по названию: от Я до А" },
                }
            };

            return viewModel;
        }

        [Route("~/Manufacturers")]
        public IActionResult Index()
        {
            return View(GetManufacturersViewModel());
        }

        [HttpPost]
        public PartialViewResult Index(int numberPage)
        {
            return PartialView("_ManufacturersTabPartial", GetManufacturersViewModel(numberPage: numberPage));
        }
    }
}
