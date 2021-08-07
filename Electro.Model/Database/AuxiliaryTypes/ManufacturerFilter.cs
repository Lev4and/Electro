using System;

namespace Electro.Model.Database.AuxiliaryTypes
{
    public class ManufacturerFilter
    {
        public bool IsUsed { get; set; }

        public Guid ManufacturerId { get; set; }
    }
}
