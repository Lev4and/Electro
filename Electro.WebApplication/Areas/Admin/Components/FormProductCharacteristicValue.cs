using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Components
{
    public class FormProductCharacteristicValue : ViewComponent
    {
        private readonly DataManager _dataManager;

        public FormProductCharacteristicValue(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(Guid productId, Guid categoryId)
        {
            var viewModel = new ProductCharacteristicValueViewModel()
            {
                UseNewValue = false,
                ProductId = productId,
                CategoryId = categoryId,
                CharacteristicId = Guid.Empty,
                NewCharacteristicValue = new CharacteristicCategoryValue(),
                ProductCharacteristicValue = new ProductCharacteristicCategoryValue() 
                {
                    ProductId = productId
                },
                Characteristics = _dataManager.Characteristics.GetCharacteristicsByCategoryId(categoryId).ToList(),
            };

            return View("Default", viewModel);
        }
    }
}
