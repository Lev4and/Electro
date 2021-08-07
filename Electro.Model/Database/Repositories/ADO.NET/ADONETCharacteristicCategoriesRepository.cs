using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETCharacteristicCategoriesRepository : ICharacteristicCategoriesRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETCharacteristicCategoriesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(Guid characteristicId, Guid categoryId, Guid sectionId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM CharacteristicCategories " +
                $"WHERE CharacteristicCategories.CharacteristicId = '{characteristicId}' AND " +
                $"  CharacteristicCategories.CategoryId = '{categoryId}' AND " +
                $"      CharacteristicCategories.SectionId = '{sectionId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool ContainsCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName)
        {
            throw new NotImplementedException();
        }

        public bool SaveCharacteristicCategory(CharacteristicCategory entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(entity.CharacteristicId,
                        entity.CategoryId, entity.SectionId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [CharacteristicCategories] (Id, CharacteristicId, CategoryId, SectionId, " +
                        $"UseWhenFiltering, UseWhenDisplayingAsBasicInformation) VALUES ('{id}', '{entity.CharacteristicId}', " +
                        $"'{entity.CategoryId}', '{entity.SectionId}', '{entity.UseWhenFiltering}', '{entity.UseWhenDisplayingAsBasicInformation}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicCategoryById(entity.Id);

                if (oldVersionEntity.CharacteristicId != entity.CharacteristicId || oldVersionEntity.CategoryId != entity.CategoryId
                        || oldVersionEntity.SectionId != entity.SectionId)
                {
                    if (!ContainsCharacteristicCategoryByCharacteristicIdAndCategoryIdAndSectionId(entity.CharacteristicId,
                            entity.CategoryId, entity.SectionId))
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

        public CharacteristicCategory GetCharacteristicCategoryById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public CharacteristicCategory GetCharacteristicCategoryByCharacteristicIdAndCategoryId(Guid characteristicId, Guid categoryId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public CharacteristicCategory GetCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(string characteristicName, string categoryName, string sectionName, bool track = false)
        {
            var query = $"SELECT TOP(1) CharacteristicCategories.Id, CharacteristicCategories.CharacteristicId, " +
                $"CharacteristicCategories.CategoryId, CharacteristicCategories.SectionId, " +
                $"CharacteristicCategories.UseWhenFiltering, CharacteristicCategories.UseWhenDisplayingAsBasicInformation " +
                $"FROM CharacteristicCategories INNER JOIN " +
                $"  Characteristics ON Characteristics.Id = CharacteristicCategories.CharacteristicId INNER JOIN " +
                $"      SectionsCharacteristics ON SectionsCharacteristics.Id = CharacteristicCategories.SectionId INNER JOIN " +
                $"          Categories ON Categories.Id = CharacteristicCategories.CategoryId " +
                $"WHERE Characteristics.Name = '{characteristicName}' AND SectionsCharacteristics.Name = '{sectionName}' AND " +
                $"Categories.Name = '{categoryName}'";

            var queryResult = _context.ExecuteQuery(query);
            var result = new CharacteristicCategory();

            if(queryResult.Rows.Count == 1)
            {
                result.Id = queryResult.Rows[0].Field<Guid>("Id");
                result.CharacteristicId = queryResult.Rows[0].Field<Guid>("CharacteristicId");
                result.CategoryId = queryResult.Rows[0].Field<Guid>("CategoryId");
                result.SectionId = queryResult.Rows[0].Field<Guid>("SectionId");
                result.UseWhenFiltering = queryResult.Rows[0].Field<bool>("UseWhenFiltering");
                result.UseWhenDisplayingAsBasicInformation = queryResult.Rows[0].Field<bool>("UseWhenDisplayingAsBasicInformation");
            }
            else
            {
                result = null;
            }

            return result;
        }

        public IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCategoryId(Guid categoryId, CatalogProductsFilters filters = null, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CharacteristicCategory> GetCharacteristicCategoriesByCharacteristicId(Guid characteristicId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteCharacteristicCategoryById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void DeleteRangeCharacteristicCategories(List<CharacteristicCategory> characteristicCategories)
        {
            throw new NotImplementedException();
        }
    }
}
