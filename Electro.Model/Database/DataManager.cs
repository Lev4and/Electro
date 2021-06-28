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

        public IQuantitiesRepository Quantities { get; private set; }

        public IQuantityUnitsRepository QuantityUnits { get; private set; }

        public IRolesRepository Roles { get; private set; }

        public IUnitsRepository Units { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(ICategoriesRepository categories, ICategoryPhotosRepository categoryPhotos,
            IManufacturersRepository manufacturers, IManufacturerInformationRepository manufacturerInformation, 
            IManufacturerLogosRepository manufacturerLogos, IQuantitiesRepository quantities, 
            IQuantityUnitsRepository quantityUnits, IRolesRepository roles, IUnitsRepository units, IUsersRepository users)
        {
            Categories = categories;
            CategoryPhotos = categoryPhotos;
            ManufacturerInformation = manufacturerInformation;
            ManufacturerLogos = manufacturerLogos;
            Manufacturers = manufacturers;
            Quantities = quantities;
            QuantityUnits = quantityUnits;
            Roles = roles;
            Units = units;
            Users = users;
        }
    }
}
