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
            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics
                .Include(sectionCharacteristic => sectionCharacteristic.Categories)
                    .ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category);

            if (!track)
            {
                sectionCharacteristics = sectionCharacteristics.AsNoTracking();
            }

            return sectionCharacteristics.SingleOrDefault(sectionsCharacteristic => sectionsCharacteristic.Id == id);
        }

        public SectionCharacteristic GetSectionCharacteristicByName(string name, bool track = false)
        {
            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics;

            if (!track)
            {
                sectionCharacteristics = sectionCharacteristics.AsNoTracking();
            }

            return sectionCharacteristics.SingleOrDefault(sectionCharacteristic => sectionCharacteristic.Name == name);
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristics(bool track = false)
        {
            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics
                .Include(sectionCharacteristic => sectionCharacteristic.Categories)
                    .ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category);

            if (!track)
            {
                sectionCharacteristics = sectionCharacteristics.AsNoTracking();
            }

            return sectionCharacteristics;
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristicsByCategoryId(Guid categoryId, bool track = false)
        {
            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics
                .Include(sectionCharacteristic => sectionCharacteristic.Categories)
                    .ThenInclude(sectionCharacteristicCategory => sectionCharacteristicCategory.Category);

            if (!track)
            {
                sectionCharacteristics = sectionCharacteristics.AsNoTracking();
            }

            return sectionCharacteristics.Where(sectionCharacteristic => 
                sectionCharacteristic.Categories.Any(sectionCharacteristicCategory =>
                    sectionCharacteristicCategory.CategoryId == categoryId) == true);
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
