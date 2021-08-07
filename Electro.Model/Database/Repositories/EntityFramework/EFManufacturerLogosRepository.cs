using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturerLogosRepository : IManufacturerLogosRepository
    {
        private readonly ElectroDbContext _context;

        public EFManufacturerLogosRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturerLogoByManufacturerId(Guid manufacturerId)
        {
            return _context.ManufacturerLogos.SingleOrDefault(manufacturerLogo => manufacturerLogo.ManufacturerId == manufacturerId) != null;
        }

        public bool SaveManufacturerLogo(ManufacturerLogo entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsManufacturerLogoByManufacturerId(entity.ManufacturerId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturerLogoById(entity.Id);

                if(oldVersionEntity.ManufacturerId != entity.ManufacturerId)
                {
                    if (!ContainsManufacturerLogoByManufacturerId(entity.ManufacturerId))
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

        public ManufacturerLogo GetManufacturerLogoById(Guid id, bool track = false)
        {
            IQueryable<ManufacturerLogo> manufacturerLogos = _context.ManufacturerLogos;

            if (!track)
            {
                manufacturerLogos = manufacturerLogos.AsNoTracking();
            }

            return manufacturerLogos.SingleOrDefault(manufacturerLogo => manufacturerLogo.Id == id);
        }

        public ManufacturerLogo GetManufacturerLogoByManufacturerId(Guid manufacturerId, bool track = false)
        {
            IQueryable<ManufacturerLogo> manufacturerLogos = _context.ManufacturerLogos;

            if (!track)
            {
                manufacturerLogos = manufacturerLogos.AsNoTracking();
            }

            return manufacturerLogos.SingleOrDefault(manufacturerLogo => manufacturerLogo.ManufacturerId == manufacturerId);
        }

        public void DeleteManufacturerLogoById(Guid id)
        {
            _context.ManufacturerLogos.Remove(GetManufacturerLogoById(id));
            _context.SaveChanges();
        }
    }
}
