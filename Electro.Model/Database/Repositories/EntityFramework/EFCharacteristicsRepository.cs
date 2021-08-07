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
            IQueryable<Characteristic> characteristics = _context.Characteristics
                .Include(characteristic => characteristic.Categories);

            if (!track)
            {
                characteristics = characteristics.AsNoTracking();
            }

            return characteristics.SingleOrDefault(characteristic => characteristic.Id == id);
        }

        public Characteristic GetCharacteristicByName(string name, bool track = false)
        {
            IQueryable<Characteristic> characteristics = _context.Characteristics;

            if (!track)
            {
                characteristics = characteristics.AsNoTracking();
            }

            return characteristics.SingleOrDefault(characteristic => characteristic.Name == name);
        }

        public IQueryable<Characteristic> GetCharacteristics(bool track = false)
        {
            IQueryable<Characteristic> characteristics = _context.Characteristics
                .Include(characteristic => characteristic.Categories);

            if (!track)
            {
                characteristics = characteristics.AsNoTracking();
            }

            return characteristics;
        }

        public IQueryable<Characteristic> GetCharacteristicsByCategoryId(Guid categoryId, bool track = false)
        {
            IQueryable<Characteristic> characteristics = _context.Characteristics
                .Include(characteristic => characteristic.Categories)
                    .ThenInclude(characteristicCategory => characteristicCategory.Values)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.Products);

            if (!track)
            {
                characteristics = characteristics.AsNoTracking();
            }

            return characteristics.Where(characteristic => characteristic.Categories.Any(characteristicCategory =>
                    characteristicCategory.CategoryId == categoryId));
        }

        public IQueryable<Characteristic> GetNotUsedCharacteristicsForProductByProductIdAndCategoryId(Guid productId, Guid categoryId, bool track = false)
        {
            IQueryable<Characteristic> characteristics = _context.Characteristics
                .Include(characteristic => characteristic.Categories)
                    .ThenInclude(characteristicCategory => characteristicCategory.Values)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.Products);

            if (!track)
            {
                characteristics = characteristics.AsNoTracking();
            }

            return characteristics.Where(characteristic => !characteristic.Categories.Any(characteristicCategory =>
                    characteristicCategory.Values.Any(characteristicCategoryValue =>
                        characteristicCategoryValue.Products.Any(productCharacteristicCategoryValue =>
                            productCharacteristicCategoryValue.ProductId == productId))) &&
                                characteristic.Categories.Any(characteristicCategory =>
                                    characteristicCategory.CategoryId == categoryId));
        }

        public void DeleteCharacteristicById(Guid id)
        {
            _context.Characteristics.Remove(GetCharacteristicById(id));
            _context.SaveChanges();
        }
    }
}
