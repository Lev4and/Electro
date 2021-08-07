using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICharacteristicCategoryValuesRepository
    {
        bool ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(Guid characteristicCategoryId, string value);

        bool SaveCharacteristicCategoryValue(CharacteristicCategoryValue entity);

        CharacteristicCategoryValue GetCharacteristicCategoryValueById(Guid id, bool track = false);

        CharacteristicCategoryValue GetCharacteristicCategoryValueByCharacteristicNameAndCategoryNameAndSectionNameAndValue(string characteristicName, string categoryName, string sectionName, string value, bool track = false);

        IQueryable<CharacteristicCategoryValue> GetCharacteristicCategoryValuesByCharacteristicId(Guid characteristicId, bool track = false);

        void DeleteRangeCharacteristicCategoryValues(List<CharacteristicCategoryValue> characteristicCategoryValues);
    }
}
