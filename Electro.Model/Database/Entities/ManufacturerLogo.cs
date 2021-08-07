using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Electro.Model.Database.Entities
{
    public class ManufacturerLogo
    {
        public Guid Id { get; set; }

        public Guid ManufacturerId { get; set; }

        [ValidateNever]
        public bool IsAbsolute { get; set; }

        [Required]
        [ValidateNever]
        public string Url { get; set; }

        public virtual Manufacturer Manufacturer { get; set; }

        public string GetPhotoUrl()
        {
            return IsAbsolute ? Url : $"~/{Url}";
        }
    }
}
