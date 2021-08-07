using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETCharacteristicCategoryValuesRepository : ICharacteristicCategoryValuesRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETCharacteristicCategoryValuesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(Guid characteristicCategoryId, string value)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM CharacteristicCategoryValues " +
                $"WHERE CharacteristicCategoryValues.CharacteristicCategoryId = '{characteristicCategoryId}' AND " +
                $"  CharacteristicCategoryValues.Value = @Value";

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@Value",
                SqlDbType = SqlDbType.NVarChar,
                SqlValue = value
            });

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public bool SaveCharacteristicCategoryValue(CharacteristicCategoryValue entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(entity.CharacteristicCategoryId,
                    entity.Value))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [CharacteristicCategoryValues] (Id, CharacteristicCategoryId, Value) VALUES " +
                        $"('{id}', '{entity.CharacteristicCategoryId}', @Value)";

                    entity.Id = id;

                    var parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Value",
                        SqlDbType = SqlDbType.NVarChar,
                        SqlValue = entity.Value
                    });

                    _context.ExecuteQuery(query, parameters);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicCategoryValueById(entity.Id);

                if (oldVersionEntity.CharacteristicCategoryId != entity.CharacteristicCategoryId ||
                    oldVersionEntity.Value != entity.Value)
                {
                    if (!ContainsCharacteristicCategoryValueByCharacteristicCategoryIdAndValue(entity.CharacteristicCategoryId,
                            entity.Value))
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

        public CharacteristicCategoryValue GetCharacteristicCategoryValueByCharacteristicNameAndCategoryNameAndSectionNameAndValue(string characteristicName, string categoryName, string sectionName, string value, bool track = false)
        {
            var query = $"SELECT TOP(1) CharacteristicCategoryValues.Id, CharacteristicCategoryValues.CharacteristicCategoryId, " +
                $"CharacteristicCategoryValues.Value " +
                $"FROM CharacteristicCategoryValues INNER JOIN " +
                $"  CharacteristicCategories ON CharacteristicCategories.Id = " +
                $"CharacteristicCategoryValues.CharacteristicCategoryId INNER JOIN " +
                $"      SectionsCharacteristics ON SectionsCharacteristics.Id = CharacteristicCategories.SectionId INNER JOIN " +
                $"          Characteristics ON Characteristics.Id = CharacteristicCategories.CharacteristicId INNER JOIN " +
                $"              Categories ON Categories.Id = CharacteristicCategories.CategoryId " +
                $"WHERE SectionsCharacteristics.Name = '{sectionName}' AND Characteristics.Name = '{characteristicName}' AND " +
                $"  Categories.Name = '{categoryName}' AND CharacteristicCategoryValues.Value = @Value";

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@Value",
                SqlDbType = SqlDbType.NVarChar,
                SqlValue = value
            });

            var queryResult = _context.ExecuteQuery(query, parameters);
            var result = new CharacteristicCategoryValue();

            if(queryResult.Rows.Count == 1)
            {
                result.Id = queryResult.Rows[0].Field<Guid>("Id");
                result.CharacteristicCategoryId = queryResult.Rows[0].Field<Guid>("CharacteristicCategoryId");
                result.Value = queryResult.Rows[0].Field<string>("Value");
            }
            else
            {
                result = null;
            }

            return result;
        }

        public CharacteristicCategoryValue GetCharacteristicCategoryValueById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<CharacteristicCategoryValue> GetCharacteristicCategoryValuesByCharacteristicId(Guid characteristicId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteRangeCharacteristicCategoryValues(List<CharacteristicCategoryValue> characteristicCategoryValues)
        {
            throw new NotImplementedException();
        }
    }
}
