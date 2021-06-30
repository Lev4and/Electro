using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFQuantityUnitsRepository : IQuantityUnitsRepository
    {
        private readonly ElectroDbContext _context;

        public EFQuantityUnitsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsQuantityUnitByQuantityIdAndUnitId(Guid quantityId, Guid unitId)
        {
            return _context.QuantityUnits.SingleOrDefault(quantityUnit => quantityUnit.QuantityId == quantityId && 
                quantityUnit.UnitId == unitId) != null;
        }

        public bool SaveQuantityUnit(QuantityUnit entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsQuantityUnitByQuantityIdAndUnitId(entity.QuantityId, entity.UnitId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetQuantityUnitById(entity.Id);

                if(oldVersionEntity.QuantityId != entity.QuantityId || oldVersionEntity.UnitId != entity.UnitId)
                {
                    if (!ContainsQuantityUnitByQuantityIdAndUnitId(entity.QuantityId, entity.UnitId))
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

        public QuantityUnit GetQuantityUnitById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .SingleOrDefault(quantityUnit => quantityUnit.Id == id);
            }
            else
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .AsNoTracking()
                    .SingleOrDefault(quantityUnit => quantityUnit.Id == id);
            }
        }

        public IQueryable<QuantityUnit> GetQuantityUnits(bool track = false)
        {
            if (track)
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic);
            }
            else
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .AsNoTracking();
            }
        }

        public IQueryable<QuantityUnit> GetNotUsedQuantityUnitsForCharacteristic(Guid characteristicId, bool track = false)
        {
            if (track)
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .Where(quantityUnit => quantityUnit.CharacteristicQuantityUnits.All(characteristicQuantityUnits =>
                        characteristicQuantityUnits.CharacteristicId != characteristicId) == true);
            }
            else
            {
                return _context.QuantityUnits
                    .Include(quantityUnit => quantityUnit.Unit)
                    .Include(quantityUnit => quantityUnit.Quantity)
                    .Include(quantityUnit => quantityUnit.CharacteristicQuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.Characteristic)
                    .AsNoTracking()
                    .Where(quantityUnit => quantityUnit.CharacteristicQuantityUnits.All(characteristicQuantityUnits =>
                        characteristicQuantityUnits.CharacteristicId != characteristicId) == true);
            }
        }

        public void DeleteQuantityUnitById(Guid id)
        {
            _context.QuantityUnits.Remove(GetQuantityUnitById(id));
            _context.SaveChanges();
        }

        public void DeleteQuantityUnit(QuantityUnit entity)
        {
            _context.QuantityUnits.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRangeQuantityUnits(List<QuantityUnit> quantityUnits)
        {
            _context.QuantityUnits.RemoveRange(quantityUnits);
            _context.SaveChanges();
        }

        public void Detach(QuantityUnit entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
