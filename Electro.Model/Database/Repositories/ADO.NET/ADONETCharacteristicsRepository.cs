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
    public class ADONETCharacteristicsRepository : ICharacteristicsRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETCharacteristicsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsCharacteristicByName(string name)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM Characteristics " +
                $"WHERE Characteristics.Name = '{name}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveCharacteristic(Characteristic entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsCharacteristicByName(entity.Name))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [Characteristics] (Id, Name, Description) VALUES ('{id}', " +
                        $"'{entity.Name}', '{entity.Description}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetCharacteristicById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsCharacteristicByName(entity.Name))
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

        public Characteristic GetCharacteristicById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public Characteristic GetCharacteristicByName(string name, bool track = false)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM Characteristics " +
                $"WHERE Characteristics.Name = '{name}'";

            var queryResult = _context.ExecuteQuery(query);
            var result = new Characteristic();

            result.Id = queryResult.Rows[0].Field<Guid>("Id");
            result.Name = queryResult.Rows[0].Field<string>("Name");
            result.Description = queryResult.Rows[0].Field<string>("Description");

            return result;
        }

        public IQueryable<Characteristic> GetCharacteristics(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Characteristic> GetCharacteristicsByCategoryId(Guid categoryId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Characteristic> GetNotUsedCharacteristicsForProductByProductIdAndCategoryId(Guid productId, Guid categoryId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteCharacteristicById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
