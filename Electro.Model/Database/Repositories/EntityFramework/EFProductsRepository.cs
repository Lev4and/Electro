using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.Product;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFProductsRepository : IProductsRepository
    {
        private readonly ElectroDbContext _context;
        private readonly IEnumerable<IProductsSorter> _sorters;

        public EFProductsRepository(ElectroDbContext context, IEnumerable<IProductsSorter> sorters)
        {
            _context = context;
            _sorters = sorters;
        }

        public bool ContainsProductByManufacturerIdAndModel(Guid manufacturerId, string model)
        {
            return _context.Products.SingleOrDefault(product => product.ManufacturerId == manufacturerId &&
                product.Model == model) != null;
        }

        public bool SaveProduct(Product entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductByManufacturerIdAndModel(entity.ManufacturerId, entity.Model))
                {
                    entity.CreatedAt = DateTime.Now;

                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductById(entity.Id);

                if (oldVersionEntity.ManufacturerId != entity.ManufacturerId || oldVersionEntity.Model != entity.Model)
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

        public int GetCountProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters)
        {
            List<Guid> manufacturers = new List<Guid>();

            if (filters.ManufacturerFilters != null ? filters.ManufacturerFilters.Count > 0 : false)
            {
                manufacturers = filters.ManufacturerFilters.Where(manufacturer =>
                    manufacturer.IsUsed == true).Select(manufacturer => manufacturer.ManufacturerId).ToList();
            }

            IQueryable<Product> products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.MainPhoto)
                .Include(product => product.Manufacturer)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Section);

            products = products.Where(product => product.CategoryId == categoryId && product.Price >= filters.RangePrice.From && 
                product.Price <= filters.RangePrice.To);

            if (manufacturers.Count > 0)
            {
                products = products.Where(product => manufacturers.Contains(product.ManufacturerId));
            }

            foreach (var characteristicFilter in filters.CharacteristicFilters)
            {
                var characteristicValues = characteristicFilter.CharacteristicValueFilters.Where(characteristicValue =>
                    characteristicValue.IsUsed == true)
                    .Select(characteristicValue => characteristicValue.CharacteristicValueId)
                    .ToList();

                if (characteristicValues.Count > 0)
                {
                    products = products.Where(product => product.CharacteristicsValues.Any(characteristicsValue =>
                        characteristicValues.Contains(characteristicsValue.CharacteristicCategoryValueId)));
                }
            }

            return products.Count();
        }

        public double GetMinPriceProductByCategoryId(Guid categoryId)
        {
            var products = _context.Products
                .Where(product => product.CategoryId == categoryId);

            if(products != null ? products.Count() > 0 : false)
            {
                return products.Min(product => product.Price);
            }

            return 0;
        }

        public double GetMaxPriceProductByCategoryId(Guid categoryId)
        {
            var products = _context.Products
                .Where(product => product.CategoryId == categoryId);

            if (products != null ? products.Count() > 0 : false)
            {
                return products.Max(product => product.Price);
            }

            return 0;
        }

        public Product GetProductById(Guid id, bool track = false)
        {
            IQueryable<Product> products = _context.Products
                .Include(product => product.Photos)
                .Include(product => product.Category)
                    .ThenInclude(category => category.Parent)
                        .ThenInclude(category => category.Parent)
                .Include(product => product.MainPhoto)
                .Include(product => product.Information)
                .Include(product => product.Manufacturer)
                    .ThenInclude(manufacturer => manufacturer.Logo)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Section);
            if (!track)
            {
                products = products.AsNoTracking();
            }

            return products.SingleOrDefault(product => product.Id == id);
        }

        public IQueryable<Product> GetProducts(bool track = false)
        {
            IQueryable<Product> products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.MainPhoto)
                .Include(product => product.Manufacturer);

            if (!track)
            {
                products = products.AsNoTracking();
            }

            return products;
        }

        public IQueryable<Product> GetProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters, bool track = false)
        {
            var sorter = _sorters.FirstOrDefault(sorter => sorter.SortingOption == filters.SortingOption);

            List<Guid> manufacturers = new List<Guid>();

            if (filters.ManufacturerFilters != null ? filters.ManufacturerFilters.Count > 0 : false)
            {
                manufacturers = filters.ManufacturerFilters.Where(manufacturer =>
                    manufacturer.IsUsed == true).Select(manufacturer => manufacturer.ManufacturerId).ToList();
            }

            var products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.MainPhoto)
                .Include(product => product.Manufacturer)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Section)
                .Where(product => product.CategoryId == categoryId && product.Price >= filters.RangePrice.From &&
                    product.Price <= filters.RangePrice.To);

            if (sorter != null)
            {
                products = sorter.Sort(products);
            }

            if (manufacturers.Count > 0)
            {
                products = products.Where(product => manufacturers.Contains(product.ManufacturerId));
            }

            foreach (var characteristicFilter in filters.CharacteristicFilters)
            {
                var characteristicValues = characteristicFilter.CharacteristicValueFilters.Where(characteristicValue =>
                    characteristicValue.IsUsed == true)
                    .Select(characteristicValue => characteristicValue.CharacteristicValueId)
                    .ToList();

                if (characteristicValues.Count > 0)
                {
                    products = products.Where(product => product.CharacteristicsValues.Any(characteristicsValue =>
                        characteristicValues.Contains(characteristicsValue.CharacteristicCategoryValueId)));
                }
            }

            products = products.Skip((filters.NumberPage - 1) * filters.ItemsPerPage)
                .Take(filters.ItemsPerPage);

            if (!track)
            {
                products = products.AsNoTracking();
            }

            return products;
        }

        public IQueryable<Product> GetLatestProductsByCategoryId(Guid categoryId, int countItemsInResult, bool track = false)
        {
            var products = _context.Products
                .Include(product => product.Category)
                .Include(product => product.MainPhoto)
                .Include(product => product.Manufacturer)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Characteristic)
                .Include(product => product.CharacteristicsValues)
                    .ThenInclude(productCharacteristicCategoryValue =>
                        productCharacteristicCategoryValue.CharacteristicCategoryValue)
                        .ThenInclude(characteristicCategoryValue => characteristicCategoryValue.CharacteristicCategory)
                            .ThenInclude(characteristicCategory => characteristicCategory.Section)
                .OrderByDescending(product => product.CreatedAt)
                .Where(product => product.CategoryId == categoryId)
                .Take(countItemsInResult);

            if (!track)
            {
                products = products.AsNoTracking();
            }

            return products;
        }

        public void DeleteProductById(Guid id)
        {
            _context.Products.Remove(GetProductById(id));
            _context.SaveChanges();
        }
    }
}