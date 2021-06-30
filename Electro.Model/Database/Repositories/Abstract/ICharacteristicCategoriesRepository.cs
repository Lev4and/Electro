using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICharacteristicCategoriesRepository
    {
        bool ContainsCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId);

        bool SaveCharacteristicCategory(CharacteristicCategory entity);

        CharacteristicCategory GetCharacteristicCategoryById(Guid id, bool track = false);

        void DeleteRangeCharacteristicCategories(List<CharacteristicCategory> characteristicCategories);
    }
}
