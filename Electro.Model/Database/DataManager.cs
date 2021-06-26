using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class DataManager
    {
        public ICategoriesRepository Categories { get; private set; }

        public ICategoryPhotosRepository CategoryPhotos { get; private set; }

        public IRolesRepository Roles { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(ICategoriesRepository categories, ICategoryPhotosRepository categoryPhotos,
            IRolesRepository roles, IUsersRepository users)
        {
            Categories = categories;
            CategoryPhotos = categoryPhotos;
            Roles = roles;
            Users = users;
        }
    }
}
