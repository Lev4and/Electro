using Electro.Model.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface IProductPhotosRepository
    {
        bool ContainsProductPhotoByProductIdAndUrl(Guid productId, string url);

        bool SaveProductPhoto(ProductPhoto entity);

        ProductPhoto GetProductPhotoById(Guid id, bool track = false);

        IQueryable<ProductPhoto> GetProductPhotosByProductId(Guid productId, bool track = false);

        IQueryable<ProductPhoto> GetNotIncludedInTheListProductPhotosByProductId(Guid productId, List<ProductPhoto> productPhotos, bool track = false);

        void DeleteProductPhotoById(Guid id);

        void DeleteRangeProductPhotos(List<ProductPhoto> productPhotos);
    }
}
