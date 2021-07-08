﻿using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductsRepository
    {
        bool ContainsProductByManufacturerIdAndModel(Guid manufacturerId, string model);

        bool SaveProduct(Product entity);

        Product GetProductById(Guid id, bool track = false);

        IQueryable<Product> GetProducts(bool track = false);

        void DeleteProductById(Guid id);
    }
}