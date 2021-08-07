using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CategoriesViewModel
    {
        public Pagination Pagination { get; set; }

        public CategoriesFilters Filters { get; set; }

        public List<Category> Categories { get; set; }
    }
}
