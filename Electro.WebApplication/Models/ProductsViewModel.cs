using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class ProductsViewModel
    {
        public Pagination Pagination { get; set; }

        public ProductsFilters Filters { get; set; }

        public List<int> Limits { get; set; }

        public List<Product> Products { get; set; }

        public List<Category> Categories { get; set; }

        public List<Manufacturer> Manufacturers { get; set; }

        public Dictionary<SortingOption, string> SortingOptions { get; set; }
    }
}
