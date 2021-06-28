using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFSectionsCharacteristicsRepository : ISectionsCharacteristicsRepository
    {
        private readonly ElectroDbContext _context;

        public EFSectionsCharacteristicsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsSectionCharacteristicByName(string name)
        {
            return _context.SectionsCharacteristics.SingleOrDefault(sectionCharacteristic =>
                sectionCharacteristic.Name == name) != null;
        }

        public bool SaveSectionCharacteristic(SectionCharacteristic entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsSectionCharacteristicByName(entity.Name))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetSectionCharacteristicById(entity.Id);

                if(oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsSectionCharacteristicByName(entity.Name))
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

        public SectionCharacteristic GetSectionCharacteristicById(Guid id, bool track = false)
        {
            if (track)
            {
                return _context.SectionsCharacteristics
                    .Include(sectionCharacteristic => sectionCharacteristic.Categories).ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category)
                    .SingleOrDefault(sectionsCharacteristic => sectionsCharacteristic.Id == id);
            }
            else
            {
                return _context.SectionsCharacteristics
                    .Include(sectionCharacteristic => sectionCharacteristic.Categories).ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category)
                    .AsNoTracking()
                    .SingleOrDefault(sectionCharacteristic => sectionCharacteristic.Id == id);
            }
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristics(bool track = false)
        {
            if (track)
            {
                return _context.SectionsCharacteristics
                    .Include(sectionCharacteristic => sectionCharacteristic.Categories).ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category);
            }
            else
            {
                return _context.SectionsCharacteristics
                    .Include(sectionCharacteristic => sectionCharacteristic.Categories).ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category)
                    .AsNoTracking();
            }
        }

        public void DeleteSectionCharacteristicById(Guid id)
        {
            _context.SectionsCharacteristics.Remove(GetSectionCharacteristicById(id));
            _context.SaveChanges();
        }

        public void Detach(SectionCharacteristic entity)
        {
            _context.SectionsCharacteristics.Remove(entity);
            _context.SaveChanges();
        }
    }
}
