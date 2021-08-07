using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class ManufacturerViewModel
    {
        public Manufacturer Manufacturer { get; set; }

        public List<CategoriesManufacturer> Categories { get; set; }
    }
}
