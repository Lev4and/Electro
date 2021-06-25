using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ProductInformation
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        public string Description { get; set; }

        public Product Product { get; set; }
    }
}
