using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class DataManager
    {
        public ICategoriesRepository Categories { get; private set; }

        public ICategoryPhotosRepository CategoryPhotos { get; private set; }

        public ICharacteristicCategoriesRepository CharacteristicCategories { get; private set; }

        public ICharacteristicCategoryValuesRepository CharacteristicCategoryValues { get; private set; }

        public ICharacteristicsRepository Characteristics { get; private set; }

        public IManufacturerInformationRepository ManufacturerInformation { get; private set; }

        public IManufacturerLogosRepository ManufacturerLogos { get; private set; }

        public IManufacturersRepository Manufacturers { get; private set; }

        public IProductCharacteristicCategoryValuesRepository ProductCharacteristicCategoryValues { get; private set; }

        public IProductInformationRepository ProductInformation { get; private set; }

        public IProductMainPhotosRepository ProductMainPhotos { get; private set; }

        public IProductPhotosRepository ProductPhotos { get; private set; }

        public IProductsRepository Products { get; private set; }

        public IRolesRepository Roles { get; private set; }

        public ISectionCharacteristicCategoriesRepository SectionCharacteristicCategories { get; private set; }

        public ISectionsCharacteristicsRepository SectionsCharacteristics { get; private set; }

        public IUsersRepository Users { get; private set; }

        public DataManager(ICategoriesRepository categories, ICategoryPhotosRepository categoryPhotos, 
            ICharacteristicCategoriesRepository characteristicCategories, 
            ICharacteristicCategoryValuesRepository characteristicCategoryValues, ICharacteristicsRepository characteristics, 
            IManufacturersRepository manufacturers, IManufacturerInformationRepository manufacturerInformation, 
            IManufacturerLogosRepository manufacturerLogos, 
            IProductCharacteristicCategoryValuesRepository productCharacteristicCategoryValues,
            IProductInformationRepository productInformation, IProductMainPhotosRepository productMainPhotos, 
            IProductPhotosRepository productPhotos, IProductsRepository products, IRolesRepository roles, 
            ISectionCharacteristicCategoriesRepository sectionCharacteristicCategories,
            ISectionsCharacteristicsRepository sectionsCharacteristics, IUsersRepository users)
        {
            Categories = categories;
            CategoryPhotos = categoryPhotos;
            CharacteristicCategories = characteristicCategories;
            CharacteristicCategoryValues = characteristicCategoryValues;
            Characteristics = characteristics;
            ManufacturerInformation = manufacturerInformation;
            ManufacturerLogos = manufacturerLogos;
            Manufacturers = manufacturers;
            ProductCharacteristicCategoryValues = productCharacteristicCategoryValues;
            ProductInformation = productInformation;
            ProductMainPhotos = productMainPhotos;
            ProductPhotos = productPhotos;
            Products = products;
            Roles = roles;
            SectionCharacteristicCategories = sectionCharacteristicCategories;
            SectionsCharacteristics = sectionsCharacteristics;
            Users = users;
        }
    }
}
