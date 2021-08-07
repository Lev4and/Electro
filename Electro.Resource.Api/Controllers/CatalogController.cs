using Electro.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Electro.Resource.Api.Controllers
{
    [ApiController]
    [Route("api/catalog")]
    [Produces("application/json")]
    public class CatalogController : ControllerBase
    {
        private readonly DataManager _dataManager;

        public CatalogController(DataManager dataManager)
        {
            _dataManager = dataManager;
        }

        public IActionResult Index()
        {
            return Ok(_dataManager.Categories.GetParentCategories().ToList());
        }
    }
}
