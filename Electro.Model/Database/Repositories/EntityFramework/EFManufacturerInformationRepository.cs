using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFManufacturerInformationRepository : IManufacturerInformationRepository
    {
        private readonly ElectroDbContext _context;

        public EFManufacturerInformationRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturerInformationByManufacturerId(Guid manufacturerId)
        {
            return _context.ManufacturerInformation.SingleOrDefault(manufacturerInformation => manufacturerInformation.ManufacturerId == manufacturerId) != null;
        }

        public bool SaveManufacturerInformation(ManufacturerInformation entity)
        {
            if(entity.Id == default)
            {
                if (!ContainsManufacturerInformationByManufacturerId(entity.Id))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturerInformationById(entity.Id);

                if(oldVersionEntity.ManufacturerId != entity.ManufacturerId)
                {
                    if (!ContainsManufacturerInformationByManufacturerId(entity.Id))
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

        public ManufacturerInformation GetManufacturerInformationById(Guid id, bool track = false)
        {
            IQueryable<ManufacturerInformation> manufacturerInformation = _context.ManufacturerInformation;

            if (!track)
            {
                manufacturerInformation = manufacturerInformation.AsNoTracking();
            }

            return manufacturerInformation.SingleOrDefault(manufacturerInformation => manufacturerInformation.Id == id);
        }

        public ManufacturerInformation GetManufacturerInformationByManufacturerId(Guid manufacturerId, bool track = false)
        {
            IQueryable<ManufacturerInformation> manufacturerInformation = _context.ManufacturerInformation;

            if (!track)
            {
                manufacturerInformation = manufacturerInformation.AsNoTracking();
            }

            return manufacturerInformation.SingleOrDefault(manufacturerInformation => manufacturerInformation.ManufacturerId == 
                manufacturerId);
        }

        public void DeleteManufacturerInformationById(Guid id)
        {
            _context.ManufacturerInformation.Remove(GetManufacturerInformationById(id));
            _context.SaveChanges();
        }
    }
}
