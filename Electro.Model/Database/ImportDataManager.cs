using Electro.Model.Database.Repositories.Abstract;

namespace Electro.Model.Database
{
    public class ImportDataManager
    {
        public ICategoriesRepository Categories { get; private set; }

        public ICharacteristicCategoriesRepository CharacteristicCategories { get; private set; }

        public ICharacteristicCategoryValuesRepository CharacteristicCategoryValues { get; private set; }

        public ICharacteristicsRepository Characteristics { get; private set; }

        public IManufacturerLogosRepository ManufacturerLogos { get; private set; }

        public IManufacturersRepository Manufacturers { get; private set; }

        public IProductCharacteristicCategoryValuesRepository ProductCharacteristicCategoryValues { get; private set; }

        public IProductInformationRepository ProductInformation { get; private set; }

        public IProductMainPhotosRepository ProductMainPhotos { get; private set; }

        public IProductPhotosRepository ProductPhotos { get; private set; }

        public IProductsRepository Products { get; private set; }

        public ISectionCharacteristicCategoriesRepository SectionCharacteristicCategories { get; private set; }

        public ISectionsCharacteristicsRepository SectionsCharacteristics { get; private set; }

        public ImportDataManager(ICategoriesRepository categories,
            ICharacteristicCategoriesRepository characteristicCategories,
            ICharacteristicCategoryValuesRepository characteristicCategoryValues, ICharacteristicsRepository characteristics,
            IManufacturersRepository manufacturers, IManufacturerLogosRepository manufacturerLogos,
            IProductCharacteristicCategoryValuesRepository productCharacteristicCategoryValues,
            IProductInformationRepository productInformation, IProductMainPhotosRepository productMainPhotos,
            IProductPhotosRepository productPhotos, IProductsRepository products,
            ISectionCharacteristicCategoriesRepository sectionCharacteristicCategories,
            ISectionsCharacteristicsRepository sectionsCharacteristics)
        {
            Categories = categories;
            CharacteristicCategories = characteristicCategories;
            CharacteristicCategoryValues = characteristicCategoryValues;
            Characteristics = characteristics;
            ManufacturerLogos = manufacturerLogos;
            Manufacturers = manufacturers;
            ProductCharacteristicCategoryValues = productCharacteristicCategoryValues;
            ProductInformation = productInformation;
            ProductMainPhotos = productMainPhotos;
            ProductPhotos = productPhotos;
            Products = products;
            SectionCharacteristicCategories = sectionCharacteristicCategories;
            SectionsCharacteristics = sectionsCharacteristics;
        }
    }
}
