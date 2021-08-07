using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICharacteristicCategoriesRepository
    {
        bool ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(Guid characteristicId, Guid categoryId, Guid sectionId);

        bool ContainsCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName);

        bool SaveCharacteristicCategory(CharacteristicCategory entity);

        CharacteristicCategory GetCharacteristicCategoryById(Guid id, bool track = false);

        CharacteristicCategory GetCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId, bool track = false);

        CharacteristicCategory GetCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName, bool track = false);

        IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCategoryId(Guid categoryId, CatalogProductsFilters filters = null, bool track = false);

        IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCharacteristicId(Guid characteristicId, bool track = false);

        void DeleteCharacteristicCategoryById(Guid id);

        void DeleteRangeCharacteristicCategories(List<CharacteristicCategory> characteristicCategories);
    }
}
