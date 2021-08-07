using Electro.Model.Database.AuxiliaryTypes;
using Electro.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace Electro.WebApplication.Components
{
    public class Filters : ViewComponent
    {
        public IViewComponentResult Invoke(CatalogProductsViewModel filters, FilterVersion version)
        {
            ViewBag.Version = version.ToString().ToLower();

            return View("Default", filters);
        }
    }
}
