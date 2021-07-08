using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ElectroDbContext _context;

        public EFProductsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductByManufacturerIdAndModel(Guid manufacturerId, string model)
        {
            return _context.Products.SingleOrDefault(product => product.ManufacturerId == manufacturerId &&
                product.Model == model) != null;
        }

        public bool SaveProduct(Product entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsProductByManufacturerIdAndModel(entity.ManufacturerId, entity.Model))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductById(entity.Id);

                if(oldVersionEntity.ManufacturerId != entity.ManufacturerId || oldVersionEntity.Model != entity.Model)
                {
                    if (!ContainsProductByManufacturerIdAndModel(entity.ManufacturerId, entity.Model))
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

        public Product GetProductById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.Products
                    .Include(product => product.Photos)
                    .Include(product => product.Category)
                    .Include(product => product.MainPhoto)
                    .Include(product => product.Information)
                    .Include(product => product.Manufacturer)
                    .Include(product => product.CharacteristicsValues)
                        .ThenInclude(productCharacteristicCategoryValue =>
                            productCharacteristicCategoryValue.CharacteristicCategoryValue)
                            .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                                .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .SingleOrDefault(product => product.Id == id);
            }
            else
            {
                return _context.Products
                    .Include(product => product.Photos)
                    .Include(product => product.Category)
                    .Include(product => product.MainPhoto)
                    .Include(product => product.Information)
                    .Include(product => product.Manufacturer)
                    .Include(product => product.CharacteristicsValues)
                        .ThenInclude(productCharacteristicCategoryValue =>
                            productCharacteristicCategoryValue.CharacteristicCategoryValue)
                            .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                                .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                    .AsNoTracking()
                    .SingleOrDefault(product => product.Id == id);

            }
        }

        public IQueryable<Product> GetProducts(bool track = false)
        {
            if (track)
            {
                return _context.Products
                    .Include(product => product.Category)
                    .Include(product => product.MainPhoto)
                    .Include(product => product.Manufacturer);
            }
            else
            {
                return _context.Products
                    .Include(product => product.Category)
                    .Include(product => product.MainPhoto)
                    .Include(product => product.Manufacturer)
                    .AsNoTracking();
            }
        }

        public void DeleteProductById(Guid id)
        {
            _context.Products.Remove(GetProductById(id));
            _context.SaveChanges();
        }
    }
}
