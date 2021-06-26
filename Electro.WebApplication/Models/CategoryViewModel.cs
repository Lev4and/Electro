using Electro.Model.Database.Entities;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CategoryViewModel
    {
        public Category Category { get; set; }

        public List<Category> Categories { get; set; }
    }
}
