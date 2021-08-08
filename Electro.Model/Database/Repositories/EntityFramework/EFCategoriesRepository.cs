using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Category;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly ElectroDbContext _context;
        private readonly IEnumerable<ICategoriesSorter> _sorters;

        public EFCategoriesRepository(ElectroDbContext context, IEnumerable<ICategoriesSorter> sorters)
        {
            _context = context;
            _sorters = sorters;
        }

        public bool ContainsCategoryByParentIdAndName(Guid? parentId, string name)
        {
            return _context.Categories.SingleOrDefault(category => category.ParentId == parentId && 
                category.Name == name) != null;
        }

        public bool SaveCategory(Category entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCategoryByParentIdAndName(entity.ParentId, entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCategoryById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsCategoryByParentIdAndName(entity.ParentId, entity.Name))
                    {
                        _context.Entry(entity).State = EntityState.Modified;
                        _context.SaveChanges();

                        return true;
                    }
                }
                else
                {
                    _context.Entry(entity).State = EntityState.Modified;
                    _context.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public int GetCountCategories(CategoriesFilters filters)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Parent).ThenInclude(category => category.Parent)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children)
                    .ThenInclude(category => category.Children)
                        .ThenInclude(category => category.Photo);

            if (filters.ParentCategoryId != null)
            {
                categories = categories.Where(category => category.ParentId == filters.ParentCategoryId);
            }

            if (!string.IsNullOrEmpty(filters.SearchString))
            {
                categories = categories.Where(category => category.Name.ToLower().Contains(filters.SearchString));
            }

            return categories.Count();
        }

        public Category GetCategoryById(Guid id, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Parent).ThenInclude(category => category.Parent)
                .Include(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Children);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.SingleOrDefault(category => category.Id == id);
        }

        public Category GetCategoryByParentNameAndName(string parentName, string name, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Parent);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.SingleOrDefault(category => category.Name == name &&
                    (category.Parent != null ? category.Parent.Name == parentName : true));
        }

        public IQueryable<Category> GetCategories(bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Parent).ThenInclude(category => category.Parent)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories;
        }

        public IQueryable<Category> GetCategories(CategoriesFilters filters, bool track = false)
        {
            var sorter = _sorters.FirstOrDefault(sorter => sorter.SortingOption == filters.SortingOption);

            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Parent).ThenInclude(category => category.Parent)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children)
                    .ThenInclude(category => category.Children)
                        .ThenInclude(category => category.Photo);

            if(filters.ParentCategoryId != null)
            {
                categories = categories.Where(category => category.ParentId == filters.ParentCategoryId);
            }

            if (!string.IsNullOrEmpty(filters.SearchString))
            {
                categories = categories.Where(category => category.Name.ToLower().Contains(filters.SearchString));
            }

            if(sorter != null)
            {
                categories = sorter.Sort(categories);
            }

            categories = categories.Skip((filters.NumberPage - 1) * filters.ItemsPerPage)
                .Take(filters.ItemsPerPage);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories;
        }

        public IQueryable<CategoriesManufacturer> GetCategoriesByManufacturerId(Guid manufacturerId, bool track = false)
        {
            IQueryable<Product> products = _context.Products
                .Include(product => product.Category)
                    .ThenInclude(category => category.Photo)
                .Where(product => product.ManufacturerId == manufacturerId);

            if(products != null ? products.Count() > 0 : false)
            {
                IQueryable<CategoriesManufacturer> categories = products.GroupBy(product => 
                new
                { 
                    product.Category.Id, 
                    product.Category.Name 
                })
                .Select(group => new CategoriesManufacturer
                {
                    Id = group.Key.Id,
                    Name = group.Key.Name,
                    CountProducts = group.Count()
                });

                if (!track)
                {
                    categories = categories.AsNoTracking();
                }

                return categories;
            }

            return new List<CategoriesManufacturer>().AsQueryable();
        }

        public IQueryable<Category> GetParentCategories(bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Children)
                    .ThenInclude(category => category.Photo);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.Where(category => category.ParentId == null);
        }

        public IQueryable<Category> GetCategories(string searchString, int itemsPerResult, bool track = false)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                IQueryable<Category> categories = _context.Categories;

                categories = categories.Where(category => category.Name.ToLower().Contains(searchString.ToLower()));
                categories = categories.OrderBy(category => category.Name).Take(itemsPerResult);

                if (!track)
                {
                    categories = categories.AsNoTracking();
                }

                return categories;
            }

            return new List<Category>().AsQueryable();
        }

        public IQueryable<Category> GetParentCategories(int numberPage, int itemsPerPage, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Children)
                    .ThenInclude(category => category.Photo);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.OrderBy(category => category.Id)
                .Where(category => category.ParentId == null)
                .Skip((numberPage - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }

        public IQueryable<Category> GetCategoriesWithoutASpecific(Guid id, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Photo)
                .Include(category => category.Parent).ThenInclude(category => category.Parent)
                .Include(category => category.Children).ThenInclude(category => category.Photo)
                .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.Where(category => category.Id != id);
        }

        public IQueryable<Category> GetNotUsedCategoriesForSectionCharacteristic(Guid sectionCharacteristicId, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.SectionsCharacteristics);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.Where(category => category.SectionsCharacteristics.All(sectionCharacteristic =>
                    sectionCharacteristic.SectionId != sectionCharacteristicId) == true);
        }

        public IQueryable<Category> GetNotUsedCategoriesForCharacteristic(Guid characteristicId, bool track = false)
        {
            IQueryable<Category> categories = _context.Categories
                .Include(category => category.Characteristics);

            if (!track)
            {
                categories = categories.AsNoTracking();
            }

            return categories.Where(category => category.Characteristics.All(characteristics =>
                    characteristics.CharacteristicId != characteristicId) == true);
        }

        public void DeleteCategoryById(Guid id)
        {
            _context.Categories.Remove(GetCategoryById(id));
            _context.SaveChanges();
        }
    }
}
