using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Newtonsoft.Json;
using System;
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

        public Dictionary<Guid, DateTime> Favorites { get; set; }

        public static Dictionary<Guid, DateTime> GetFavoritesByJsonString(string jsonString)
        {
            if (!string.IsNullOrEmpty(jsonString))
            {
                try
                {
                    return JsonConvert.DeserializeObject<Dictionary<Guid, DateTime>>(jsonString);
                }
                catch
                {
                    return new Dictionary<Guid, DateTime>();
                }
            }

            return new Dictionary<Guid, DateTime>();
        }

        public bool ContainsProductInFavorite(Guid productId)
        {
            if(Favorites != null)
            {
                return Favorites.ContainsKey(productId);
            }

            return false;
        }
    }
}
