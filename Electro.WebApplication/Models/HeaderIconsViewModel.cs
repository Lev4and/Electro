using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class HeaderIconsViewModel
    {
        public int CountProductsInCart { get; set; }

        public int CountProductsInCompare { get; set; }

        public int CountProductsInFavorites { get; set; }

        public List<Product> CompareContent { get; set; }

        public Dictionary<Product, int> CartContent { get; set; }
    }
}
