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
                    .SingleOrDefault(characteristic => characteristic.Id == id);
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .AsNoTracking()
                    .SingleOrDefault(characteristic => characteristic.Id == id);
            }
        }

        public IQueryable<Characteristic> GetCharacteristics(bool track = false)
        {
            if (track)
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories);
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .AsNoTracking();
            }
        }

        public IQueryable<Characteristic> GetCharacteristicsByCategoryId(Guid categoryId, bool track = false)
        {
            if (track)
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .Where(characteristic => characteristic.Categories.Any(characteristicCategory => 
                        characteristicCategory.CategoryId == categoryId));
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                    .AsNoTracking()
                    .Where(characteristic => characteristic.Categories.Any(characteristicCategory => 
                        characteristicCategory.CategoryId == categoryId));
            }
        }

        public IQueryable<Characteristic> GetNotUsedCharacteristicsForProductByProductIdAndCategoryId(Guid productId, Guid categoryId, bool track = false)
        {
            if (track)
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                        .ThenInclude(characteristicCategory => characteristicCategory.Values)
                            .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.Products)
                    .Where(characteristic => !characteristic.Categories.Any(characteristicCategory =>
                        characteristicCategory.Values.Any(characteristicCategoryValue =>
                            characteristicCategoryValue.Products.Any(productCharacteristicCategoryValue =>
                                productCharacteristicCategoryValue.ProductId == productId))) &&
                                    characteristic.Categories.Any(characteristicCategory =>
                                        characteristicCategory.CategoryId == categoryId));
            }
            else
            {
                return _context.Characteristics
                    .Include(characteristic => characteristic.Categories)
                        .ThenInclude(characteristicCategory => characteristicCategory.Values)
                            .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.Products)
                    .AsNoTracking()
                    .Where(characteristic => !characteristic.Categories.Any(characteristicCategory =>
                        characteristicCategory.Values.Any(characteristicCategoryValue =>
                            characteristicCategoryValue.Products.Any(productCharacteristicCategoryValue =>
                                productCharacteristicCategoryValue.ProductId == productId))) &&
                                    characteristic.Categories.Any(characteristicCategory =>
                                        characteristicCategory.CategoryId == categoryId));
            }
        }

        public void DeleteCharacteristicById(Guid id)
        {
            _context.Characteristics.Remove(GetCharacteristicById(id));
            _context.SaveChanges();
        }
    }
}
