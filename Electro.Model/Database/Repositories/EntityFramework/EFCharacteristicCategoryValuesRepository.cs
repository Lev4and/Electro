using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCharacteristicCategoryValuesRepository : ICharacteristicCategoryValuesRepository
    {
        private readonly ElectroDbContext _context;

        public EFCharacteristicCategoryValuesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(Guid characteristicCategoryId, string value)
        {
            return _context.CharacteristicCategoryValues.SingleOrDefault(characteristicCategoryValue =>
                characteristicCategoryValue.CharacteristicCategoryId == characteristicCategoryId &&
                    characteristicCategoryValue.Value == value) != null;
        }

        public bool SaveCharacteristicCategoryValue(CharacteristicCategoryValue entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(entity.CharacteristicCategoryId,
                    entity.Value))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicCategoryValueById(entity.Id);

                if(oldVersionEntity.CharacteristicCategoryId != entity.CharacteristicCategoryId || 
                    oldVersionEntity.Value != entity.Value)
                {
                    if (!ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(entity.CharacteristicCategoryId,
                            entity.Value))
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

        public CharacteristicCategoryValue GetCharacteristicCategoryValueById(Guid id, bool track = false)
        {
            IQueryable<CharacteristicCategoryValue> characteristicCategoryValues = _context.CharacteristicCategoryValues;

            if (!track)
            {
                characteristicCategoryValues = characteristicCategoryValues.AsNoTracking();
            }

            return characteristicCategoryValues.SingleOrDefault(characteristicCategoryValue =>
                    characteristicCategoryValue.Id == id);
        }

        public CharacteristicCategoryValue GetCharacteristicCategoryValueByCharacteristicNameAndCategoryNameAndSectionNameAndValue(string characteristicName, string categoryName, string sectionName, string value, bool track = false)
        {
            IQueryable<CharacteristicCategoryValue> characteristicCategoryValues = _context.CharacteristicCategoryValues
                .Include(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                    .ThenInclude(characteristicCategory => characteristicCategory.Section)
                .Include(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                    .ThenInclude(characteristicCategory => characteristicCategory.Category)
                .Include(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                    .ThenInclude(characteristicCategory => characteristicCategory.Characteristic);

            if (!track)
            {
                characteristicCategoryValues = characteristicCategoryValues.AsNoTracking();
            }

            return characteristicCategoryValues.SingleOrDefault(characteristicCategoryValue =>
                characteristicCategoryValue.CharacteristicCategory.Section.Name == sectionName &&
                    characteristicCategoryValue.CharacteristicCategory.Category.Name == categoryName &&
                        characteristicCategoryValue.CharacteristicCategory.Characteristic.Name == characteristicName &&
                            characteristicCategoryValue.Value == value);
        }

        public IQueryable<CharacteristicCategoryValue> GetCharacteristicCategoryValuesByCharacteristicId(Guid characteristicId, bool track = false)
        {
            IQueryable<CharacteristicCategoryValue> characteristicCategoryValues = _context.CharacteristicCategoryValues
                .Include(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                    .ThenInclude(characteristicCategory => characteristicCategory.Characteristic);

            if (!track)
            {
                characteristicCategoryValues = characteristicCategoryValues.AsNoTracking();
            }

            return characteristicCategoryValues.Where(characteristicCategoryValue =>
                    characteristicCategoryValue.CharacteristicCategory.CharacteristicId == characteristicId);
        }

        public void DeleteRangeCharacteristicCategoryValues(List<CharacteristicCategoryValue> characteristicCategoryValues)
        {
            _context.CharacteristicCategoryValues.RemoveRange(characteristicCategoryValues);
            _context.SaveChanges();
        }
    }
}
