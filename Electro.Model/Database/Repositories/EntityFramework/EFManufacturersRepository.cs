﻿using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturersRepository : IManufacturersRepository
    {
        private readonly ElectroDbContext _context;

        public EFManufacturersRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturerByName(string name)
        {
            return _context.Manufacturers.SingleOrDefault(manufacturer => manufacturer.Name == name) != null;
        }

        public bool SaveManufacturer(Manufacturer entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsManufacturerByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturerById(entity.Id);

                if(oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsManufacturerByName(entity.Name))
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

        public Manufacturer GetManufacturerById(Guid id, bool track = false)
        {
            IQueryable<Manufacturer> manufacturers = _context.Manufacturers
                .Include(manufacturer => manufacturer.Logo)
                .Include(manufacturer => manufacturer.Information);

            if (!track)
            {
                manufacturers = manufacturers.AsNoTracking();
            }

            return manufacturers.SingleOrDefault(manufacturer => manufacturer.Id == id);
        }

        public Manufacturer GetManufacturerByName(string name, bool track = false)
        {
            IQueryable<Manufacturer> manufacturers = _context.Manufacturers;

            if (!track)
            {
                manufacturers = manufacturers.AsNoTracking();
            }

            return manufacturers.SingleOrDefault(manufacturer => manufacturer.Name == name);
        }

        public IQueryable<Manufacturer> GetManufacturers(bool track = false)
        {
            IQueryable<Manufacturer> manufacturers = _context.Manufacturers
                .Include(manufacturer => manufacturer.Logo)
                .Include(manufacturer => manufacturer.Information);

            if (!track)
            {
                manufacturers = manufacturers.AsNoTracking();
            }

            return manufacturers;
        }

        public IQueryable<ProductsManufacturer> GetManufacturersByCategoryId(Guid categoryId, bool track = false)
        {
            IQueryable<Product> products = _context.Products.Include(product => product.Manufacturer)
                .Where(product => product.CategoryId == categoryId);

            if (products != null ? products.Count() > 0 : false)
            {
                IQueryable<ProductsManufacturer> productsManufacturers = products.GroupBy(product => new { product.Manufacturer.Id, product.Manufacturer.Name })
                    .Select(group => new ProductsManufacturer
                    {
                        CountProducts = group.Count(),
                        ManufacturerId = group.Key.Id,
                        ManufacturerName = group.Key.Name
                    });

                if (!track)
                {
                    productsManufacturers = productsManufacturers.AsNoTracking();
                }

                return productsManufacturers;
            }

            return new List<ProductsManufacturer>().AsQueryable();
        }

        public void DeleteManufacturerById(Guid id)
        {
            _context.Manufacturers.Remove(GetManufacturerById(id));
            _context.SaveChanges();
        }
    }
}
