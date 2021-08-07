using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCharacteristicCategoriesRepository : ICharacteristicCategoriesRepository
    {
        private readonly ElectroDbContext _context;

        public EFCharacteristicCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(Guid characteristicId, Guid categoryId, Guid sectionId)
        {
            return _context.CharacteristicCategories.SingleOrDefault(characteristicCategory => characteristicCategory.CharacteristicId == characteristicId 
                && characteristicCategory.CategoryId == categoryId && characteristicCategory.SectionId == sectionId) != null;
        }

        public bool ContainsCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName)
        {
            return _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Section)
                .Include(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategory => characteristicCategory.Characteristic)
                .SingleOrDefault(characteristicCategory => characteristicCategory.Section.Name == sectionName &&
                    characteristicCategory.Category.Name == categoryName && characteristicCategory.Characteristic.Name ==
                        characteristicName) != null;
        }

        public bool SaveCharacteristicCategory(CharacteristicCategory entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(entity.CharacteristicId, 
                        entity.CategoryId, entity.SectionId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicCategoryById(entity.Id);

                if(oldVersionEntity.CharacteristicId != entity.CharacteristicId || oldVersionEntity.CategoryId != entity.CategoryId 
                        || oldVersionEntity.SectionId != entity.SectionId)
                {
                    if (!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(entity.CharacteristicId, 
                            entity.CategoryId, entity.SectionId))
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

        public CharacteristicCategory GetCharacteristicCategoryById(Guid id, bool track = false)
        {
            IQueryable<CharacteristicCategory> characteristicCategories = _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Characteristic)
                .Include(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategory => characteristicCategory.Section);

            if (!track)
            {
                characteristicCategories = characteristicCategories.AsNoTracking();
            }

            return characteristicCategories.SingleOrDefault(characteristicCategory => characteristicCategory.Id == id);
        }

        public CharacteristicCategory GetCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId, bool track = false)
        {
            IQueryable<CharacteristicCategory> characteristicCategories = _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Characteristic)
                .Include(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategory => characteristicCategory.Section);

            if (!track)
            {
                characteristicCategories = characteristicCategories.AsNoTracking();
            }

            return characteristicCategories.SingleOrDefault(characteristicCategory =>
                    characteristicCategory.CharacteristicId == characteristicId &&
                        characteristicCategory.CategoryId == categoryId);
        }

        public CharacteristicCategory GetCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName, bool track = false)
        {
            IQueryable<CharacteristicCategory> characteristicCategories = _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Section)
                .Include(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategory => characteristicCategory.Characteristic);

            if (!track)
            {
                characteristicCategories = characteristicCategories.AsNoTracking();
            }

            return characteristicCategories.SingleOrDefault(characteristicCategory => characteristicCategory.Section.Name == 
                sectionName && characteristicCategory.Category.Name == categoryName && 
                    characteristicCategory.Characteristic.Name == characteristicName);
        }

        public IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCategoryId(Guid categoryId, CatalogProductsFilters filters = null, bool track = false)
        {
            var characteristicCategories = _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Characteristic)
                .Include(characteristicCategory => characteristicCategory.Values)
                .Where(characteristicCategory => characteristicCategory.CategoryId == categoryId);

            if (track)
            {
                characteristicCategories = characteristicCategories.AsNoTracking();
            }

            return characteristicCategories;
        }

        public IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCharacteristicId(Guid characteristicId, bool track = false)
        {
            IQueryable<CharacteristicCategory> characteristicCategories = _context.CharacteristicCategories
                .Include(characteristicCategory => characteristicCategory.Characteristic)
                .Include(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategory => characteristicCategory.Section);

            if (!track)
            {
                characteristicCategories = characteristicCategories.AsNoTracking();
            }

            return characteristicCategories.Where(characteristicCategory => characteristicCategory.CharacteristicId == 
                characteristicId);
        }

        public void DeleteCharacteristicCategoryById(Guid id)
        {
            _context.CharacteristicCategories.Remove(GetCharacteristicCategoryById(id));
            _context.SaveChanges();
        }

        public void DeleteRangeCharacteristicCategories(List<CharacteristicCategory> characteristicCategories)
        {
            _context.CharacteristicCategories.RemoveRange(characteristicCategories);
            _context.SaveChanges();
        }
    }
}
