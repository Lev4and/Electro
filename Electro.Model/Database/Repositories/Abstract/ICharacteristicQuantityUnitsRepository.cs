using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICharacteristicQuantityUnitsRepository
    {
        bool ContainsCharacteristicQuantityUnitByCharacteristicIdAndQuantityUnitId(Guid characteristicId, Guid quantityUnitId);

        bool SaveCharacteristicQuantityUnit(CharacteristicQuantityUnit entity);

        CharacteristicQuantityUnit GetCharacteristicQuantityUnitById(Guid id, bool track = false);

        void DeleteRangeCharacteristicQuantityUnits(List<CharacteristicQuantityUnit> characteristicQuantityUnits);
    }
}
