using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class ProductViewModel
    {
        public Product Product { get; set; }

        public List<Category> Categories { get; set; }

        public List<Manufacturer> Manufacturers { get; set; }
    }
}
