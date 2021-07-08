using Electro.Model.Database;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Components
{
    public class DataTableProductCharacteristicValues : ViewComponent
    {
        private readonly DataManager _dataManager;

        public DataTableProductCharacteristicValues(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(Guid productId)
        {
            var viewModel = new ProductCharacteristicValuesViewModel()
            {
                ProductCharacteristicValues = _dataManager.ProductCharacteristicCategoryValues
                    .GetProductCharacteristicCategoryValuesByProductId(productId).ToList()
            };

            return View("Default", viewModel);
        }
    }
}
