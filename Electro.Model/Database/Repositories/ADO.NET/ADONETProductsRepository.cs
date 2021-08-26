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
    public class ADONETProductsRepository : IProductsRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETProductsRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductByManufacturerIdAndModel(Guid manufacturerId, string model)
        {
            //var query = $"SELECT TOP(1) * " +
            //    $"FROM Products " +
            //    $"WHERE Products.ManufacturerId = '{manufacturerId}' AND Products.Model = '{model}'";

            //return _context.ExecuteQuery(query).Rows.Count > 0;

            return false;
        }

        public bool SaveProduct(Product entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductByManufacturerIdAndModel(entity.ManufacturerId, entity.Model))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO Products (Id, CategoryId, ManufacturerId, Model, Price, CreatedAt) VALUES ('{id}', " +
                        $"'{entity.CategoryId}', '{entity.ManufacturerId}', @Model, @Price, @CreatedAt)";

                    entity.Id = id;

                    var parameters = new List<SqlParameter>();

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Model",
                        SqlDbType = SqlDbType.NVarChar,
                        SqlValue = entity.Model
                    });

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@Price",
                        SqlDbType = SqlDbType.Float,
                        SqlValue = entity.Price
                    });

                    parameters.Add(new SqlParameter()
                    {
                        ParameterName = "@CreatedAt",
                        SqlDbType = SqlDbType.DateTime2,
                        SqlValue = entity.CreatedAt
                    });

                    _context.ExecuteQuery(query, parameters);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductById(entity.Id);

                if (oldVersionEntity.ManufacturerId != entity.ManufacturerId || oldVersionEntity.Model != entity.Model)
                {
                    if (!ContainsProductByManufacturerIdAndModel(entity.ManufacturerId, entity.Model))
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

        public int GetCountProducts(ProductsFilters filters)
        {
            throw new NotImplementedException();
        }

        public int GetCountProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters)
        {
            throw new NotImplementedException();
        }

        public double GetMinPriceProductByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public double GetMaxPriceProductByCategoryId(Guid categoryId)
        {
            throw new NotImplementedException();
        }

        public Product GetProductById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetProducts(bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetProducts(ProductsFilters filters, bool isLiteVersion = true, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetLatestProductsByCategoryId(Guid categoryId, int countItemsInResult, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Product> GetProductsByIds(List<Guid> ids, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
