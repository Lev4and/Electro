using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class DataManager
    {
        public ICategoriesRepository Categories { get; private set; }

        public ICategoryPhotosRepository CategoryPhotos { get; private set; }

        public ICharacteristicCategoriesRepository CharacteristicCategories { get; private set; }

        public ICharacteristicQuantityUnitsRepository CharacteristicQuantityUnits { get; private set; }

        public ICharacteristicsRepository Characteristics { get; private set; }

        public IManufacturerInformationRepository ManufacturerInformation { get; private set; }

        public IManufacturerLogosRepository ManufacturerLogos { get; private set; }

        public IManufacturersRepository Manufacturers { get; private set; }

        public IQuantitiesRepository Quantities { get; private set; }

        public IQuantityUnitsRepository QuantityUnits { get; private set; }

        public IRolesRepository Roles { get; private set; }

        public ISectionCharacteristicCategoriesRepository SectionCharacteristicCategories { get; private set; }

        public ISectionsCharacteristicsRepository SectionsCharacteristics { get; private set; }

        public IUnitsRepository Units { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(ICategoriesRepository categories, ICategoryPhotosRepository categoryPhotos, 
            ICharacteristicCategoriesRepository characteristicCategories, 
            ICharacteristicQuantityUnitsRepository characteristicQuantityUnits, ICharacteristicsRepository characteristics, 
            IManufacturersRepository manufacturers, IManufacturerInformationRepository manufacturerInformation, 
            IManufacturerLogosRepository manufacturerLogos, IQuantitiesRepository quantities, 
            IQuantityUnitsRepository quantityUnits, IRolesRepository roles, 
            ISectionCharacteristicCategoriesRepository sectionCharacteristicCategories,
            ISectionsCharacteristicsRepository sectionsCharacteristics, IUnitsRepository units, IUsersRepository users)
        {
            Categories = categories;
            CategoryPhotos = categoryPhotos;
            CharacteristicCategories = characteristicCategories;
            CharacteristicQuantityUnits = characteristicQuantityUnits;
            Characteristics = characteristics;
            ManufacturerInformation = manufacturerInformation;
            ManufacturerLogos = manufacturerLogos;
            Manufacturers = manufacturers;
            Quantities = quantities;
            QuantityUnits = quantityUnits;
            Roles = roles;
            SectionCharacteristicCategories = sectionCharacteristicCategories;
            SectionsCharacteristics = sectionsCharacteristics;
            Units = units;
            Users = users;
        }
    }
}
