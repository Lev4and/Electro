using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCharacteristicQuantityUnitsRepository : ICharacteristicQuantityUnitsRepository
    {
        private readonly ElectroDbContext _context;

        public EFCharacteristicQuantityUnitsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicQuantityUnitByCharacteristicIdAndQuantityUnitId(Guid characteristicId, Guid quantityUnitId)
        {
            return _context.CharacteristicQuantityUnits.SingleOrDefault(characteristicQuantityUnit => characteristicQuantityUnit.CharacteristicId == characteristicId 
                && characteristicQuantityUnit.QuantityUnitId == quantityUnitId) != null;
        }

        public bool SaveCharacteristicQuantityUnit(CharacteristicQuantityUnit entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsCharacteristicQuantityUnitByCharacteristicIdAndQuantityUnitId(entity.CharacteristicId, entity.QuantityUnitId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicQuantityUnitById(entity.Id);

                if(oldVersionEntity.CharacteristicId != entity.CharacteristicId || 
                        oldVersionEntity.QuantityUnitId == entity.QuantityUnitId)
                {
                    if (!ContainsCharacteristicQuantityUnitByCharacteristicIdAndQuantityUnitId(entity.CharacteristicId, entity.QuantityUnitId))
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

        public CharacteristicQuantityUnit GetCharacteristicQuantityUnitById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.CharacteristicQuantityUnits
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                        .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                        .ThenInclude(quantityUnit => quantityUnit.Unit)
                    .SingleOrDefault(characteristicQuantityUnit => characteristicQuantityUnit.Id == id);
            }
            else
            {
                return _context.CharacteristicQuantityUnits
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                        .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                        .ThenInclude(quantityUnit => quantityUnit.Unit)
                    .AsNoTracking()
                    .SingleOrDefault(characteristicQuantityUnit => characteristicQuantityUnit.Id == id);
            }
        }

        public void DeleteRangeCharacteristicQuantityUnits(List<CharacteristicQuantityUnit> characteristicQuantityUnits)
        {
            _context.CharacteristicQuantityUnits.RemoveRange(characteristicQuantityUnits);
            _context.SaveChanges();
        }
    }
}
