using Electro.Model.Database.Entities;
using System;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductCharacteristicCategoryValuesRepository
    {
        bool ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(Guid productId, Guid characteristicCategoryValueId);

        bool SaveProductCharacteristicCategoryValue(ProductCharacteristicCategoryValue entity);

        ProductCharacteristicCategoryValue GetProductCharacteristicCategoryValueById(Guid id, bool track = false);

        IQueryable<ProductCharacteristicCategoryValue> GetProductCharacteristicCategoryValuesByProductId(Guid productId, bool track = false);

        void DeleteProductCharacteristicCategoryValueById(Guid id);
    }
}
