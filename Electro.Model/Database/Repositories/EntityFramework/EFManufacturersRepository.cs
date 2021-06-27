using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
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
            if (track)
            {
                return _context.Manufacturers
                    .Include(manufacturer => manufacturer.Logo)
                    .Include(manufacturer => manufacturer.Information)
                    .SingleOrDefault(manufacturer => manufacturer.Id == id);
            }
            else
            {
                return _context.Manufacturers
                    .Include(manufacturer => manufacturer.Logo)
                    .Include(manufacturer => manufacturer.Information)
                    .AsNoTracking()
                    .SingleOrDefault(manufacturer => manufacturer.Id == id);
            }
        }

        public IQueryable<Manufacturer> GetManufacturers(bool track = false)
        {
            if (track)
            {
                return _context.Manufacturers
                    .Include(manufacturer => manufacturer.Logo)
                    .Include(manufacturer => manufacturer.Information);
            }
            else
            {
                return _context.Manufacturers
                    .Include(manufacturer => manufacturer.Logo)
                    .Include(manufacturer => manufacturer.Information)
                    .AsNoTracking();
            }
        }

        public void DeleteManufacturerById(Guid id)
        {
            _context.Manufacturers.Remove(GetManufacturerById(id));
            _context.SaveChanges();
        }
    }
}
