using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCategoriesRepository : ICategoriesRepository
    {
        private readonly ElectroDbContext _context;

        public EFCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
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

        public Category GetCategoryById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Categories
                    .Include(category => category.Parent)
                    .Include(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .SingleOrDefault(category => category.Id == id);
            }
            else
            {
                return _context.Categories
                    .Include(category => category.Parent)
                    .Include(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .AsNoTracking()
                    .SingleOrDefault(category => category.Id == id);
            }
        }

        public IQueryable<Category> GetCategories(bool track = false)
        {
            if (track)
            {
                return _context.Categories
                    .Include(category => category.Photo)
                    .Include(category => category.Parent).ThenInclude(category => category.Parent)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo);
            }
            else
            {
                return _context.Categories
                    .Include(category => category.Photo)
                    .Include(category => category.Parent).ThenInclude(category => category.Parent)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo)
                    .AsNoTracking();
            }
        }

        public IQueryable<Category> GetCategoriesWithoutASpecific(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Categories
                    .Include(category => category.Photo)
                    .Include(category => category.Parent).ThenInclude(category => category.Parent)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo)
                    .Where(category => category.Id != id);
            }
            else
            {
                return _context.Categories
                    .Include(category => category.Photo)
                    .Include(category => category.Parent).ThenInclude(category => category.Parent)
                    .Include(category => category.Children).ThenInclude(category => category.Photo)
                    .Include(category => category.Children).ThenInclude(category => category.Children).ThenInclude(category => category.Photo)
                    .AsNoTracking()
                    .Where(category => category.Id != id);
            }
        }

        public IQueryable<Category> GetNotUsedCategoriesForSectionCharacteristic(Guid sectionCharacteristicId, bool track = false)
        {
            if (track)
            {
                return _context.Categories
                    .Include(category => category.SectionsCharacteristics)
                    .Where(category => category.SectionsCharacteristics.All(sectionCharacteristic => 
                        sectionCharacteristic.SectionId != sectionCharacteristicId) == true);
            }
            else
            {
                return _context.Categories
                    .Include(category => category.SectionsCharacteristics)
                    .AsNoTracking()
                    .Where(category => category.SectionsCharacteristics.All(sectionCharacteristic => 
                        sectionCharacteristic.SectionId != sectionCharacteristicId) == true);
            }
        }

        public IQueryable<Category> GetNotUsedCategoriesForCharacteristic(Guid characteristicId, bool track = false)
        {
            if (track)
            {
                return _context.Categories
                    .Include(category => category.Characteristics)
                    .Where(category => category.Characteristics.All(characteristics =>
                        characteristics.CharacteristicId != characteristicId) == true);
            }
            else
            {
                return _context.Categories
                    .Include(category => category.Characteristics)
                    .AsNoTracking()
                    .Where(category => category.Characteristics.All(characteristics =>
                        characteristics.CharacteristicId != characteristicId) == true);
            }
        }

        public void DeleteCategoryById(Guid id)
        {
            _context.Categories.Remove(GetCategoryById(id));
            _context.SaveChanges();
        }
    }
}
