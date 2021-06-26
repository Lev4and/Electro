using Electro.Model.Database.Entities;
using System;

namespace Electro.Model.Database.Repositories.Abstract
{
    public interface ICategoryPhotosRepository
    {
        bool ContainsCategoryPhotoByCategoryId(Guid categoryId);

        bool SaveCategoryPhoto(CategoryPhoto entity);

        CategoryPhoto GetCategoryPhotoById(Guid id, bool track = false);

        void DeleteCategoryPhotoById(Guid id);
    }
}
