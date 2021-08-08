using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Data;
using System.Linq;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETCategoriesRepository : ICategoriesRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCategoryByParentIdAndName(Guid? parentId, string name)
        {
            throw new NotImplementedException();
        }

        public bool SaveCategory(Category entity)
        {
            throw new NotImplementedException();
        }

        public int GetCountCategories(CategoriesFilters filters)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public Category GetCategoryByParentNameAndName(string parentName, string name, bool track = false)
        {
            var query = $"SELECT TOP(1) c.Id, c.Name " +
                $"FROM Categories AS c LEFT JOIN " +
                $"	Categories AS pc ON pc.Id = c.ParentId " +
                $"WHERE pc.Name = '{parentName}' AND c.Name = '{name}'";

            var queryResult = _context.ExecuteQuery(query);
            var result = new Category();

            result.Id = queryResult.Rows[0].Field<Guid>("Id");
            result.Name = queryResult.Rows[0].Field<string>("Name");


            return result;
        }

        public IQueryable<Category> GetCategories(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetCategories(string searchString, int itemsPerResult, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetCategories(CategoriesFilters filters, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CategoriesManufacturer> GetCategoriesByManufacturerId(Guid manufacturerId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetParentCategories(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetParentCategories(int numberPage, int itemsPerPage, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetCategoriesWithoutASpecific(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetNotUsedCategoriesForCharacteristic(Guid characteristicId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Category> GetNotUsedCategoriesForSectionCharacteristic(Guid sectionCharacteristicId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteCategoryById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
