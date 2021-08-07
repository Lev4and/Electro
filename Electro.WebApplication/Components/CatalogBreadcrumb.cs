using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Electro.WebApplication.Components
{
    public class CatalogBreadcrumb : ViewComponent
    {
        private readonly DataManager _dataManager;

        public CatalogBreadcrumb(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(Guid categoryId)
        {
            return View("Default", _dataManager.Categories.GetCategoryById(categoryId));
        }
    }
}
