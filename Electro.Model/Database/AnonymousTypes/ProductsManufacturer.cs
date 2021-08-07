using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.AnonymousTypes
{
    public class ProductsManufacturer
    {
        public int CountProducts { get; set; }

        public string ManufacturerName { get; set; }

        public Guid ManufacturerId { get; set; }
    }
}
