using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ISectionCharacteristicCategoriesRepository
    {
        bool ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(Guid sectionCharacteristicId, Guid categoryId);

        bool SaveSectionCharacteristicCategory(SectionCharacteristicCategory entity);

        SectionCharacteristicCategory GetSectionCharacteristicCategoryById(Guid id, bool track = false);

        void DeleteSectionCharacteristicCategoryById(Guid id);

        void DeleteSectionCharacteristicCategory(SectionCharacteristicCategory entity);

        void DeleteRangeSectionCharacteristicCategories(List<SectionCharacteristicCategory> sectionCharacteristicCategories);

        void Detach(SectionCharacteristicCategory entity);


    }
}
