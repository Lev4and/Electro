using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class FavoritesViewModel
    {
        public Dictionary<Product, DateTime> Content { get; set; }
    }
}
