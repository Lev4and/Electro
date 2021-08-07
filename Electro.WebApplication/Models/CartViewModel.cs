using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CartViewModel
    {
        public Dictionary<Product, int> Content { get; set; }
    }
}
