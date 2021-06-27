using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class DataManager
    {
        public ICategoriesRepository Categories { get; private set; }

        public ICategoryPhotosRepository CategoryPhotos { get; private set; }

        public IManufacturerInformationRepository ManufacturerInformation { get; private set; }

        public IManufacturerLogosRepository ManufacturerLogos { get; private set; }

        public IManufacturersRepository Manufacturers { get; private set; }

        public IRolesRepository Roles { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(ICategoriesRepository categories, ICategoryPhotosRepository categoryPhotos,
            IManufacturersRepository manufacturers, IManufacturerInformationRepository manufacturerInformation, 
            IManufacturerLogosRepository manufacturerLogos, IRolesRepository roles, IUsersRepository users)
        {
            Categories = categories;
            CategoryPhotos = categoryPhotos;
            ManufacturerInformation = manufacturerInformation;
            ManufacturerLogos = manufacturerLogos;
            Manufacturers = manufacturers;
            Roles = roles;
            Users = users;
        }
    }
}
