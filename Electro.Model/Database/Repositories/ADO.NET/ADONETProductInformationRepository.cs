using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETProductInformationRepository : IProductInformationRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETProductInformationRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductInformationByProductId(Guid productId, bool track = false)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM ProductInformation " +
                $"WHERE ProductInformation.ProductId = '{productId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveProductInformation(ProductInformation entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductInformationByProductId(entity.ProductId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [ProductInformation] (Id, ProductId, Description) VALUES ('{id}', " +
                        $"'{entity.ProductId}', @Description)";

                    entity.Id = id;
                    //entity.Description = entity.Description.Replace("'", @"\'");

                    var parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Description",
                        SqlDbType = SqlDbType.NVarChar,
                        SqlValue = entity.Description
                    });

                    _context.ExecuteQuery(query, parameters);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductInformationById(entity.Id);

                if (oldVersionEntity.ProductId != entity.ProductId)
                {
                    if (!ContainsProductInformationByProductId(entity.ProductId))
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

        public ProductInformation GetProductInformationById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public ProductInformation GetProductInformationByProductId(Guid productId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductInformationById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
