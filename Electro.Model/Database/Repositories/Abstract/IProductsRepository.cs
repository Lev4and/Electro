using Electro.Model.Database.AuxiliaryTypes;
using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductsRepository
    {
        bool ContainsProductByManufacturerIdAndModel(Guid manufacturerId, string model);

        bool SaveProduct(Product entity);

        int GetCountProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters);

        double GetMinPriceProductByCategoryId(Guid categoryId);

        double GetMaxPriceProductByCategoryId(Guid categoryId);

        Product GetProductById(Guid id, bool track = false);

        IQueryable<Product> GetProducts(bool track = false);

        IQueryable<Product> GetProductsByCategoryId(Guid categoryId, CatalogProductsFilters filters, bool track = false);

        IQueryable<Product> GetLatestProductsByCategoryId(Guid categoryId, int countItemsInResult, bool track = false);

        void DeleteProductById(Guid id);
    }
}
