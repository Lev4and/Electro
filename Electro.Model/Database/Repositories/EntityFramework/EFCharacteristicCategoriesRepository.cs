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

        public bool ContainsCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId)
        {
            return _context.CharacteristicCategories.SingleOrDefault(characteristicCategory => characteristicCategory.CharacteristicId == characteristicId 
                && characteristicCategory.CategoryId == categoryId) != null;
        }

        public bool SaveCharacteristicCategory(CharacteristicCategory entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryId(entity.CharacteristicId, entity.CategoryId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicCategoryById(entity.Id);

                if(oldVersionEntity.CharacteristicId != entity.CharacteristicId || oldVersionEntity.CategoryId != entity.CategoryId)
                {
                    if (!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryId(entity.CharacteristicId, entity.CategoryId))
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
                    .SingleOrDefault(characteristicCategory => characteristicCategory.Id == id);
            }
            else
            {
                return _context.CharacteristicCategories
                    .Include(characteristicCategory => characteristicCategory.Characteristic)
                    .Include(characteristicCategory => characteristicCategory.Category)
                    .AsNoTracking()
                    .SingleOrDefault(characteristicCategory => characteristicCategory.Id == id);
            }
        }

        public void DeleteRangeCharacteristicCategories(List<CharacteristicCategory> characteristicCategories)
        {
            _context.CharacteristicCategories.RemoveRange(characteristicCategories);
            _context.SaveChanges();
        }
    }
}
