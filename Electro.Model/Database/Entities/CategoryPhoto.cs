using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class CategoryPhoto
    {
        public Guid Id { get; set; }

        public Guid CategoryId { get; set; }

        [Required]
        public string Url { get; set; }

        public Category Category { get; set; }
    }
}
