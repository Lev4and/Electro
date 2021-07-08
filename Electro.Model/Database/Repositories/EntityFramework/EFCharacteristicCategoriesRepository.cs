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
            if (track)
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .SingleOrDefault(characteristicCategory => characteristicCategory.Id == id);
            }
            else
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .AsNoTracking()
                    .SingleOrDefault(characteristicCategory => characteristicCategory.Id == id);
            }
        }

        public CharacteristicCategory GetCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId, bool track = false)
        {
            if (track)
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .SingleOrDefault(characteristicCategory => 
                        characteristicCategory.CharacteristicId == characteristicId && 
                            characteristicCategory.CategoryId == categoryId);
            }
            else
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .AsNoTracking()
                    .SingleOrDefault(characteristicCategory =>
                        characteristicCategory.CharacteristicId == characteristicId &&
                            characteristicCategory.CategoryId == categoryId);
            }
        }

        public IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCharacteristicId(Guid characteristicId, bool track = false)
        {
            if (track)
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .Where(characteristicCategory => characteristicCategory.CharacteristicId == characteristicId);
            }
            else
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .Include(characteristicCategory => characteristicCategory.Section)
                    .AsNoTracking()
                    .Where(characteristicCategory => characteristicCategory.CharacteristicId == characteristicId);
            }
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
