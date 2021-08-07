using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ProductInformation
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        [Required]
        [ValidateNever]
        public string Description { get; set; }

        public virtual Product Product { get; set; }
    }
}
