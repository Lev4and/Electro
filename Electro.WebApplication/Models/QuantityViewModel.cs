using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class QuantityViewModel
    {
        public Quantity Quantity { get; set; }

        public List<Guid> SelectedUnits { get; set; }

        public List<Unit> UsedUnits { get; set; }

        public List<Unit> NotUsedUnitsForQuantity { get; set; }
    }
}
