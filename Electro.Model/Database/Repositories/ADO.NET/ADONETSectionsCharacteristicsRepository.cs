using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Data;
using System.Linq;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETSectionsCharacteristicsRepository : ISectionsCharacteristicsRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETSectionsCharacteristicsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsSectionCharacteristicByName(string name)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM SectionsCharacteristics " +
                $"WHERE SectionsCharacteristics.Name = '{name}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveSectionCharacteristic(SectionCharacteristic entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsSectionCharacteristicByName(entity.Name))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [SectionsCharacteristics] (Id, Name) VALUES ('{id}', '{entity.Name}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetSectionCharacteristicById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsSectionCharacteristicByName(entity.Name))
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

        public int GetCountSectionsCharacteristics(SectionsCharacteristicsFilters filters)
        {
            throw new NotImplementedException();
        }

        public SectionCharacteristic GetSectionCharacteristicById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public SectionCharacteristic GetSectionCharacteristicByName(string name, bool track = false)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM SectionsCharacteristics " +
                $"WHERE SectionsCharacteristics.Name = '{name}'";

            var queryResult = _context.ExecuteQuery(query);
            var result = new SectionCharacteristic();

            result.Id = queryResult.Rows[0].Field<Guid>("Id");
            result.Name = queryResult.Rows[0].Field<string>("Name");

            return result;
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristics(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristics(SectionsCharacteristicsFilters filters, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<SectionCharacteristic> GetSectionsCharacteristicsByCategoryId(Guid categoryId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void Detach(SectionCharacteristic entity)
        {
            throw new NotImplementedException();
        }

        public void DeleteSectionCharacteristicById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
