using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICategoriesRepository
    {
        bool ContainsCategoryByParentIdAndName(Guid? parentId, string name);

        bool SaveCategory(Category entity);

        int GetCountCategories(CategoriesFilters filters);

        Category GetCategoryById(Guid id, bool track = false);

        Category GetCategoryByParentNameAndName(string parentName, string name, bool track = false);

        IQueryable<Category> GetCategories(bool track = false);

        IQueryable<Category> GetCategories(CategoriesFilters filters, bool track = false);

        IQueryable<CategoriesManufacturer> GetCategoriesByManufacturerId(Guid manufacturerId, bool track = false);

        IQueryable<Category> GetParentCategories(bool track = false);

        IQueryable<Category> GetParentCategories(int numberPage, int itemsPerPage, bool track = false);

        IQueryable<Category> GetCategoriesWithoutASpecific(Guid id, bool track = false);

        IQueryable<Category> GetNotUsedCategoriesForSectionCharacteristic(Guid sectionCharacteristicId, bool track = false);

        IQueryable<Category> GetNotUsedCategoriesForCharacteristic(Guid characteristicId, bool track = false);

        void DeleteCategoryById(Guid id);
    }
}
