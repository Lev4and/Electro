﻿using System;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class CharacteristicValueFilter
    {
        public bool IsUsed { get; set; }

        public int Counter { get; set; }

        public Guid CharacteristicValueId { get; set; }
    }
}
