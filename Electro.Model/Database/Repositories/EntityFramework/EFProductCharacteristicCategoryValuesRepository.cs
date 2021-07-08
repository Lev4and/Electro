using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductCharacteristicCategoryValuesRepository : IProductCharacteristicCategoryValuesRepository
    {
        private readonly ElectroDbContext _context;

        public EFProductCharacteristicCategoryValuesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(Guid productId, Guid characteristicCategoryValueId)
        {
            return _context.ProductCharacteristicCategoryValues
                .SingleOrDefault(productCharacteristicCategoryValue =>
                    productCharacteristicCategoryValue.ProductId == productId &&
                        productCharacteristicCategoryValue.CharacteristicCategoryValueId == characteristicCategoryValueId) != null;
        }

        public bool SaveProductCharacteristicCategoryValue(ProductCharacteristicCategoryValue entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(entity.ProductId,
                    entity.CharacteristicCategoryValueId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductCharacteristicCategoryValueById(entity.Id);

                if(oldVersionEntity.ProductId != entity.ProductId || oldVersionEntity.CharacteristicCategoryValueId !=
                    entity.CharacteristicCategoryValueId)
                {
                    if (!ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(entity.ProductId,
                        entity.CharacteristicCategoryValueId))
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

        public ProductCharacteristicCategoryValue GetProductCharacteristicCategoryValueById(Guid id, bool track = false)
        {
            if (track) 
            {
                return _context.ProductCharacteristicCategoryValues
                    .Include(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .SingleOrDefault(productCharacteristicQuantityUnitValue =>
                        productCharacteristicQuantityUnitValue.Id == id);
            }
            else
            {
                return _context.ProductCharacteristicCategoryValues
                    .Include(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .AsNoTracking()
                    .SingleOrDefault(productCharacteristicQuantityUnitValue =>
                        productCharacteristicQuantityUnitValue.Id == id);
            }
        }

        public IQueryable<ProductCharacteristicCategoryValue> GetProductCharacteristicCategoryValuesByProductId(Guid productId, bool track = false)
        {
            if (track)
            {
                return _context.ProductCharacteristicCategoryValues
                    .Include(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .Where(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.ProductId == productId);
            }
            else
            {
                return _context.ProductCharacteristicCategoryValues
                    .Include(productCharacteristicCategoryValue => productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .AsNoTracking()
                    .Where(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.ProductId == productId);
            }
        }

        public void DeleteProductCharacteristicCategoryValueById(Guid id)
        {
            _context.ProductCharacteristicCategoryValues.Remove(GetProductCharacteristicCategoryValueById(id));
            _context.SaveChanges();
        }
    }
}
