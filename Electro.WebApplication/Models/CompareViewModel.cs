using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CompareViewModel
    {
        public List<Product> Products { get; set; }
        
        public List<CharacteristicCategory> CharacteristicCategories { get; set; }
    }
}
