using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductMainPhotosRepository
    {
        bool ContainsProductMainPhotoByProductId(Guid productId);

        bool SaveProductMainPhoto(ProductMainPhoto entity);

        ProductMainPhoto GetProductMainPhotoById(Guid id, bool track = false);

        ProductMainPhoto GetProductMainPhotoByProductId(Guid productId, bool track = false);

        void DeleteProductMainPhotoById(Guid id);
    }
}
