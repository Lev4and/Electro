using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Electro.Model.Database.Repositories.EntityFramework.Sorters.SectionsCharacteristic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFSectionsCharacteristicsRepository : ISectionsCharacteristicsRepository
    {
        private readonly ElectroDbContext _context;
        private readonly IEnumerable<ISectionsCharacteristicsSorter> _sorters;

        public EFSectionsCharacteristicsRepository(ElectroDbContext context, IEnumerable<ISectionsCharacteristicsSorter> sorters)
        {
            _context = context;
            _sorters = sorters;
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

        public int GetCountSectionsCharacteristics(SectionsCharacteristicsFilters filters)
        {
            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics;

            if (!string.IsNullOrEmpty(filters.SearchString))
            {
                sectionCharacteristics = sectionCharacteristics.Where(sectionCharacteristic =>
                    sectionCharacteristic.Name.ToLower().Contains(filters.SearchString.ToLower()));
            }

            return sectionCharacteristics.Count();
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

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristics(SectionsCharacteristicsFilters filters, bool track = false)
        {
            var sorter = _sorters.FirstOrDefault(sorter => sorter.SortingOption == filters.SortingOption);

            IQueryable<SectionCharacteristic> sectionCharacteristics = _context.SectionsCharacteristics;

            if (!string.IsNullOrEmpty(filters.SearchString))
            {
                sectionCharacteristics = sectionCharacteristics.Where(sectionCharacteristic =>
                    sectionCharacteristic.Name.ToLower().Contains(filters.SearchString.ToLower()));
            }

            if(sorter != null)
            {
                sectionCharacteristics = sorter.Sort(sectionCharacteristics);
            }

            sectionCharacteristics = sectionCharacteristics.Skip((filters.NumberPage - 1) * filters.ItemsPerPage)
                .Take(filters.ItemsPerPage);

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
