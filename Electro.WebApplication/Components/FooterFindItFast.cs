using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Electro.WebApplication.Components
{
    public class FooterFindItFast : ViewComponent
    {
        private readonly DataManager _dataManager;

        public FooterFindItFast(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IViewComponentResult Invoke(int numberPage, int itemsPerPage)
        {
            return View("Default", _dataManager.Categories.GetParentCategories(numberPage, itemsPerPage).ToList());
        }
    }
}
