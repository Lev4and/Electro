using Electro.Model.Database.Entities;
using Electro.Model.Database.Repositories.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electro.Model.Database.Repositories.ADONET
{
    public class ADONETProductCharacteristicCategoryValuesRepository : IProductCharacteristicCategoryValuesRepository
    {
        private readonly ElectroDbContext _context;

        public ADONETProductCharacteristicCategoryValuesRepository(ElectroDbContext context)
        {
            _context = context;
        }

        public bool ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(Guid productId, Guid characteristicCategoryValueId)
        {
            var query = $"SELECT TOP(1) * " +
                $"FROM ProductCharacteristicCategoryValues " +
                $"WHERE ProductCharacteristicCategoryValues.ProductId = '{productId}' AND " +
                $"  ProductCharacteristicCategoryValues.CharacteristicCategoryValueId = '{characteristicCategoryValueId}'";

            return _context.ExecuteQuery(query).Rows.Count > 0;
        }

        public bool SaveProductCharacteristicCategoryValue(ProductCharacteristicCategoryValue entity)
        {
            if (entity.Id == default)
            {
                if (!ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(entity.ProductId,
                    entity.CharacteristicCategoryValueId))
                {
                    var id = Guid.NewGuid();
                    var query = $"INSERT INTO [ProductCharacteristicCategoryValues] (Id, ProductId, CharacteristicCategoryValueId) " +
                        $"VALUES ('{id}', '{entity.ProductId}', '{entity.CharacteristicCategoryValueId}')";

                    entity.Id = id;

                    _context.ExecuteQuery(query);

                    return true;
                }
            }
            else
            {
                var oldVersionEntity = GetProductCharacteristicCategoryValueById(entity.Id);

                if (oldVersionEntity.ProductId != entity.ProductId || oldVersionEntity.CharacteristicCategoryValueId !=
                    entity.CharacteristicCategoryValueId)
                {
                    if (!ContainsProductCharacteristicCategoryValueByProductIdAndCharacteristicCategoryValueId(entity.ProductId,
                        entity.CharacteristicCategoryValueId))
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

        public ProductCharacteristicCategoryValue GetProductCharacteristicCategoryValueById(Guid id, bool track = false)
        {
            throw new NotImplementedException();
        }

        public IQueryable<ProductCharacteristicCategoryValue> GetProductCharacteristicCategoryValuesByProductId(Guid productId, bool track = false)
        {
            throw new NotImplementedException();
        }

        public void DeleteProductCharacteristicCategoryValueById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
