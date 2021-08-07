using System;
using System.Collections.Generic;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class CharacteristicFilter
    {
        public Guid CharacteristicId { get; set; }

        public List<CharacteristicValueFilter> CharacteristicValueFilters { get; set; }
    }
}
