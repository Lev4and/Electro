using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.EntityFramework
{
    public class EFSectionCharacteristicCategoriesRepository : ISectionCharacteristicCategoriesRepository
    {
        private readonly ElectroDbContext _context;

        public EFSectionCharacteristicCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(Guid sectionCharacteristicId, Guid categoryId)
        {
            return _context.SectionCharacteristicCategories.SingleOrDefault(sectionCharacteristicCategory => 
                sectionCharacteristicCategory.SectionId == sectionCharacteristicId && 
                    sectionCharacteristicCategory.CategoryId == categoryId) != null;
        }

        public bool SaveSectionCharacteristicCategory(SectionCharacteristicCategory entity)
        {
            if(entity.Id == default)
            {
                if(!ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(entity.SectionId, entity.CategoryId))
                {
                    _context.Entry(entity).State = EntityState.Added;
                    _context.SaveChanges();

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetSectionCharacteristicCategoryById(entity.Id);

                if(oldVersionEntity.SectionId != entity.SectionId || oldVersionEntity.CategoryId != entity.CategoryId)
                {
                    if (!ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(entity.SectionId, entity.CategoryId))
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

        public SectionCharacteristicCategory GetSectionCharacteristicCategoryById(Guid id, bool track = false)
        {
            IQueryable<SectionCharacteristicCategory> sectionCharacteristicCategories = _context.SectionCharacteristicCategories
                .Include(sectionCharacteristicCategory => sectionCharacteristicCategory.Section)
                .Include(sectionCharacteristicCategory => sectionCharacteristicCategory.Category);

            if (!track)
            {
                sectionCharacteristicCategories = sectionCharacteristicCategories.AsNoTracking();
            }

            return sectionCharacteristicCategories.SingleOrDefault(sectionCharacteristicCategory => 
                sectionCharacteristicCategory.Id == id);
        }

        public void DeleteSectionCharacteristicCategoryById(Guid id)
        {
            _context.SectionCharacteristicCategories.Remove(GetSectionCharacteristicCategoryById(id));
            _context.SaveChanges();
        }

        public void DeleteSectionCharacteristicCategory(SectionCharacteristicCategory entity)
        {
            _context.SectionCharacteristicCategories.Remove(entity);
            _context.SaveChanges();
        }

        public void DeleteRangeSectionCharacteristicCategories(List<SectionCharacteristicCategory> sectionCharacteristicCategories)
        {
            _context.SectionCharacteristicCategories.RemoveRange(sectionCharacteristicCategories);
            _context.SaveChanges();
        }

        public void Detach(SectionCharacteristicCategory entity)
        {
            _context.Entry(entity).State = EntityState.Detached;
        }
    }
}
