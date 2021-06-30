using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.WebApplication.Models
{
    public class CharacteristicViewModel
    {
        public Characteristic Characteristic { get; set; }

        public List<Guid> SelectedQuantityUnits { get; set; }

        public List<QuantityUnit> UsedQuantityUnits { get; set; }

        public List<QuantityUnit> NotUsedQuantityUnitsForCharacteristic { get; set; }
    }
}
