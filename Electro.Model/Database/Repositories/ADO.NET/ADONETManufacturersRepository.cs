using Electro.Model.Database.AnonymousTypes;
using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETManufacturersRepository : IManufacturersRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETManufacturersRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsManufacturerByName(string name)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM Manufacturers " +
                $"WHERE Manufacturers.Name = @Name";
            
            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                SqlValue = name
            });

            return _context.ExecuteQuery(query, parameters).Rows.Count > 0;
        }

        public bool SaveManufacturer(Manufacturer entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsManufacturerByName(entity.Name))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO Manufacturers (Id, Name) VALUES ('{id}', @Name)";

                    entity.Id = id;

                    var parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Name",
                        SqlDbType = SqlDbType.NVarChar,
                        SqlValue = entity.Name
                    });

                    _context.ExecuteQuery(query, parameters);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetManufacturerById(entity.Id);

                if (oldVersionEntity.Name != entity.Name)
                {
                    if (!ContainsManufacturerByName(entity.Name))
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

        public int GetCountManufacturers(ManufacturersFilters filters)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetManufacturerById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public Manufacturer GetManufacturerByName(string name, bool track = false)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM Manufacturers " +
                $"WHERE Manufacturers.Name = @Name";

            var parameters = new List<SqlParameter>();

            parameters.Add(new SqlParameter()
            {
                ParameterName = "@Name",
                SqlDbType = SqlDbType.NVarChar,
                SqlValue = name
            });

            var queryResult = _context.ExecuteQuery(query, parameters);
            var result = new Manufacturer();

            result.Id = queryResult.Rows[0].Field<Guid>("Id");
            result.Name = queryResult.Rows[0].Field<string>("Name");


            return result;
        }

        public IQueryable<Manufacturer> GetManufacturers(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Manufacturer> GetManufacturers(string searchString, int itemsPerResult, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Manufacturer> GetManufacturers(ManufacturersFilters filters, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductsManufacturer> GetManufacturersByCategoryId(Guid categoryId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteManufacturerById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
