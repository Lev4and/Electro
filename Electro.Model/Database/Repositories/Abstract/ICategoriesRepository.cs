using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICategoriesRepository
    {
        bool ContainsCategoryByParentIdAndName(Guid? parentId, string name);

        bool SaveCategory(Category entity);

        Category GetCategoryById(Guid id, bool track = false);

        IQueryable<Category> GetCategories(bool track = false);

        IQueryable<Category> GetCategoriesWithoutASpecific(Guid id, bool track = false);

        IQueryable<Category> GetNotUsedCategoriesForSectionCharacteristic(Guid sectionCharacteristicId, bool track = false);

        void DeleteCategoryById(Guid id);
    }
}
