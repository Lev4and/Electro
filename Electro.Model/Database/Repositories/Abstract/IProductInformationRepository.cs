using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductInformationRepository
    {
        bool ContainsProductInformationByProductId(Guid productId, bool track = false);

        bool SaveProductInformation(ProductInformation entity);

        ProductInformation GetProductInformationById(Guid id, bool track = false);

        ProductInformation GetProductInformationByProductId(Guid productId, bool track = false);

        void DeleteProductInformationById(Guid id);
    }
}
