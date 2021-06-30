using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFCharacteristicsRepository : ICharacteristicsRepository
    {
        private readonly ElectroDbContext _context;

        public EFCharacteristicsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicByName(string name)
        {
            return _context.Characteristics.SingleOrDefault(characteristic => characteristic.Name == name) != null;
        }

        public bool SaveCharacteristic(Characteristic entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsCharacteristicByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicById(entity.Id);

                if(oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsCharacteristicByName(entity.Name))
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

        public Characteristic GetCharacteristicById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Unit)
                    .SingleOrDefault(characteristic => characteristic.Id == id);
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Unit)
                    .AsNoTracking()
                    .SingleOrDefault(characteristic => characteristic.Id == id);
            }
        }

        public IQueryable<Characteristic> GetCharacteristics(bool track = false)
        {
            if (track)
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Unit);
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Quantity)
                    .Include(characteristic => characteristic.QuantityUnits)
                        .ThenInclude(characteristicQuantityUnit => characteristicQuantityUnit.QuantityUnit)
                            .ThenInclude(quantityUnit => quantityUnit.Unit)
                    .AsNoTracking();
            }
        }

        public void DeleteCharacteristicById(Guid id)
        {
            _context.Characteristics.Remove(GetCharacteristicById(id));
            _context.SaveChanges();
        }
    }
}
