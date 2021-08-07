using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETSectionCharacteristicCategoriesRepository : ISectionCharacteristicCategoriesRepository
    {
        private readonly ElectroDbContext _context;
        
        public ADONETSectionCharacteristicCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(Guid sectionCharacteristicId, Guid categoryId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM SectionCharacteristicCategories " +
                    $"WHERE SectionCharacteristicCategories.SectionId = '{sectionCharacteristicId}' AND " +
                        $"SectionCharacteristicCategories.CategoryId = '{categoryId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveSectionCharacteristicCategory(SectionCharacteristicCategory entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(entity.SectionId, entity.CategoryId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [SectionCharacteristicCategories] (Id, SectionId, CategoryId) VALUES " +
                        $"('{id}', '{entity.SectionId}', '{entity.CategoryId}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetSectionCharacteristicCategoryById(entity.Id);

                if (oldVersionEntity.SectionId != entity.SectionId || oldVersionEntity.CategoryId != entity.CategoryId)
                {
                    if (!ContainsSectionCharacteristicCategoryBySectionCharacteristicIdAndCategoryId(entity.SectionId, entity.CategoryId))
                    {
                        //TODO: Обновление не предусматривается

                        return true;
                    }
                }
                else
                {
                    //TODO: Обновление не предусматривается

                    return true;
                }
            }

            return false;
        }

        public SectionCharacteristicCategory GetSectionCharacteristicCategoryById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void Detach(SectionCharacteristicCategory entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteSectionCharacteristicCategoryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteSectionCharacteristicCategory(SectionCharacteristicCategory entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteRangeSectionCharacteristicCategories(List<SectionCharacteristicCategory> sectionCharacteristicCategories)
        {
            throw new NotImplementedException();
        }
    }
}
