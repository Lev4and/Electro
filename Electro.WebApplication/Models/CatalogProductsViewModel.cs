using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CatalogProductsViewModel
    {
        public Category Category { get; set; }

        public Pagination Pagination { get; set; }

        public CatalogProductsFilters Filters { get; set; }

        public List<int> Limits { get; set; }

        public List<Product> Products { get; set; }

        public List<Product> LatestProducts { get; set; }

        public List<ProductsManufacturer> ProductsManufacturers { get; set; }

        public List<CharacteristicCategory> CharacteristicCategories { get; set; }

        public Dictionary<SortingOption, string> SortingOptions { get; set; }
    }
}
