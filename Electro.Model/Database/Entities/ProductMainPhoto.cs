using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ProductMainPhoto
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public string Url { get; set; }

        public Product Product { get; set; }
    }
}
