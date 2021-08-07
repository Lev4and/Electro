using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Electro.Resource.Api.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly DataManager _dataManager;
        private readonly ImportDataManager _importDataManager;

        public ProductController(DataManager dataManager, ImportDataManager importDataManager)
        {
            _dataManager = dataManager;
            _importDataManager = importDataManager;
        }

        #region SaveProduct
        private bool SaveProduct(Product product)
        {
            var category = new Category();

            try
            {
                category = _importDataManager.Categories.GetCategoryByParentNameAndName(product.Category.Parent.Name,
                    product.Category.Name);
            }
            catch
            {
                return false;
            }

            var manufacturer = product.Manufacturer;
            var saveProductResult = false;

            SaveManufacturer(manufacturer);

            product.CategoryId = category.Id;
            product.ManufacturerId = manufacturer.Id;

            try
            {
                saveProductResult = _importDataManager.Products.SaveProduct(product);
            }
            catch
            {
                saveProductResult = _importDataManager.Products.SaveProduct(product);
            }

            if (saveProductResult)
            {
                SaveProductInformation(product);
                SaveProductMainPhoto(product);
                SaveProductPhotos(product);

                SaveProductCharacteristicsValues(product);

                return true;
            }

            return false;
        }

        private void SaveManufacturer(Manufacturer manufacturer)
        {
            if (string.IsNullOrEmpty(manufacturer.Name))
            {
                manufacturer.Name = "Неизвестно";
            }

            if (_importDataManager.Manufacturers.SaveManufacturer(manufacturer))
            {
                if (manufacturer.Logo != null ? !string.IsNullOrEmpty(manufacturer.Logo.Url) : false)
                {
                    manufacturer.Logo.ManufacturerId = manufacturer.Id;

                    try
                    {
                        _importDataManager.ManufacturerLogos.SaveManufacturerLogo(manufacturer.Logo);
                    }
                    catch
                    {
                        _importDataManager.ManufacturerLogos.SaveManufacturerLogo(manufacturer.Logo);
                    }
                }
            }
            else
            {
                try
                {
                    manufacturer.Id = _importDataManager.Manufacturers.GetManufacturerByName(manufacturer.Name).Id;
                }
                catch
                {
                    manufacturer.Id = _importDataManager.Manufacturers.GetManufacturerByName(manufacturer.Name).Id;
                }
            }
        }

        private void SaveProductCharacteristicsValues(Product product)
        {
            if (product.CharacteristicsValues != null ? product.CharacteristicsValues.Count > 0 : false)
            {
                foreach (var productCharacteristicValue in product.CharacteristicsValues)
                {
                    CharacteristicCategoryValue characteristicCategoryValue = null;

                    try
                    {
                        characteristicCategoryValue = _importDataManager.CharacteristicCategoryValues
                            .GetCharacteristicCategoryValueByCharacteristicNameAndCategoryNameAndSectionNameAndValue(
                                productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                    .Characteristic.Name,
                                        product.Category.Name,
                                            productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                                .Section.Name,
                                                    productCharacteristicValue.CharacteristicCategoryValue.Value);
                    }
                    catch
                    {
                        characteristicCategoryValue = _importDataManager.CharacteristicCategoryValues
                            .GetCharacteristicCategoryValueByCharacteristicNameAndCategoryNameAndSectionNameAndValue(
                                productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                    .Characteristic.Name,
                                        product.Category.Name,
                                            productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                                .Section.Name,
                                                    productCharacteristicValue.CharacteristicCategoryValue.Value);
                    }

                    if (characteristicCategoryValue != null)
                    {
                        try
                        {
                            SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                productCharacteristicValue);
                        }
                        catch
                        {
                            SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                productCharacteristicValue);
                        }
                    }
                    else
                    {
                        CharacteristicCategory characteristicCategory = null;

                        try
                        {
                            characteristicCategory = _importDataManager.CharacteristicCategories
                                .GetCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(
                                    productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                        .Characteristic.Name,
                                            product.Category.Name,
                                                productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                                    .Section.Name);
                        }
                        catch
                        {
                            characteristicCategory = _importDataManager.CharacteristicCategories
                                .GetCharacteristicCategoryByCharacteristicNameAndCategoryNameAndSectionName(
                                    productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                        .Characteristic.Name,
                                            product.Category.Name,
                                                productCharacteristicValue.CharacteristicCategoryValue.CharacteristicCategory
                                                    .Section.Name);
                        }

                        if (characteristicCategory != null)
                        {
                            try
                            {
                                characteristicCategoryValue = SaveCharacteristicCategoryValue(characteristicCategory.Id,
                                    productCharacteristicValue.CharacteristicCategoryValue.Value, characteristicCategoryValue);
                            }
                            catch
                            {
                                characteristicCategoryValue = SaveCharacteristicCategoryValue(characteristicCategory.Id,
                                    productCharacteristicValue.CharacteristicCategoryValue.Value, characteristicCategoryValue);
                            }

                            try
                            {
                                SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                    productCharacteristicValue);
                            }
                            catch
                            {
                                SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                    productCharacteristicValue);
                            }
                        }
                        else
                        {
                            var section = productCharacteristicValue.CharacteristicCategoryValue
                                .CharacteristicCategory.Section;
                            var characteristic = productCharacteristicValue.CharacteristicCategoryValue
                                .CharacteristicCategory.Characteristic;

                            try
                            {
                                section = SaveSectionCharacteristic(section);
                            }
                            catch
                            {
                                section = SaveSectionCharacteristic(section);
                            }

                            try
                            {
                                characteristic = SaveCharacteristic(characteristic);
                            }
                            catch
                            {
                                characteristic = SaveCharacteristic(characteristic);
                            }

                            try
                            {
                                SaveSectionCharacteristicCategory(section.Id, product.CategoryId);
                            }
                            catch
                            {
                                SaveSectionCharacteristicCategory(section.Id, product.CategoryId);
                            }

                            try
                            {
                                characteristicCategory = SaveCharacteristicCategory(section.Id, product.CategoryId, characteristic.Id,
                                    characteristicCategory);
                            }
                            catch
                            {
                                characteristicCategory = SaveCharacteristicCategory(section.Id, product.CategoryId, characteristic.Id,
                                    characteristicCategory);
                            }

                            try
                            {
                                characteristicCategoryValue = SaveCharacteristicCategoryValue(characteristicCategory.Id,
                                    productCharacteristicValue.CharacteristicCategoryValue.Value, characteristicCategoryValue);
                            }
                            catch
                            {
                                characteristicCategoryValue = SaveCharacteristicCategoryValue(characteristicCategory.Id,
                                    productCharacteristicValue.CharacteristicCategoryValue.Value, characteristicCategoryValue);
                            }

                            try
                            {
                                SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                    productCharacteristicValue);
                            }
                            catch
                            {
                                SaveProductCharacteristicCategoryValue(product.Id, characteristicCategoryValue.Id,
                                    productCharacteristicValue);
                            }

                        }
                    }
                }
            }
        }

        private void SaveProductCharacteristicCategoryValue(Guid productId, Guid characteristicCategoryValueId,
            ProductCharacteristicCategoryValue value)
        {
            value.ProductId = productId;
            value.CharacteristicCategoryValueId = characteristicCategoryValueId;

            _importDataManager.ProductCharacteristicCategoryValues.SaveProductCharacteristicCategoryValue(value);
        }

        private CharacteristicCategoryValue SaveCharacteristicCategoryValue(Guid characteristicCategoryId, string value,
            CharacteristicCategoryValue characteristicCategoryValue)
        {
            characteristicCategoryValue = new CharacteristicCategoryValue()
            {
                CharacteristicCategoryId = characteristicCategoryId,
                Value = value
            };

            _importDataManager.CharacteristicCategoryValues
                .SaveCharacteristicCategoryValue(characteristicCategoryValue);

            return characteristicCategoryValue;
        }

        private void SaveSectionCharacteristicCategory(Guid sectionId, Guid categoryId)
        {
            var sectionCategory = new SectionCharacteristicCategory()
            {
                SectionId = sectionId,
                CategoryId = categoryId
            };

            _importDataManager.SectionCharacteristicCategories.SaveSectionCharacteristicCategory(sectionCategory);
        }

        private SectionCharacteristic SaveSectionCharacteristic(SectionCharacteristic section)
        {
            if (!_importDataManager.SectionsCharacteristics.SaveSectionCharacteristic(section))
            {
                section = _importDataManager.SectionsCharacteristics.GetSectionCharacteristicByName(section.Name);
            }

            return section;
        }

        private CharacteristicCategory SaveCharacteristicCategory(Guid sectionId, Guid categoryId, Guid characteristicId,
            CharacteristicCategory characteristicCategory)
        {
            characteristicCategory = new CharacteristicCategory()
            {
                SectionId = sectionId,
                CategoryId = categoryId,
                UseWhenFiltering = false,
                CharacteristicId = characteristicId,
                UseWhenDisplayingAsBasicInformation = false
            };

            _importDataManager.CharacteristicCategories.SaveCharacteristicCategory(characteristicCategory);

            return characteristicCategory;
        }

        private Characteristic SaveCharacteristic(Characteristic characteristic)
        {
            if (!_importDataManager.Characteristics.SaveCharacteristic(characteristic))
            {
                characteristic = _importDataManager.Characteristics.GetCharacteristicByName(characteristic.Name);
            }

            return characteristic;
        }

        private void SaveProductInformation(Product product)
        {
            if (product.Information != null ? !string.IsNullOrEmpty(product.Information.Description) : false)
            {
                product.Information.ProductId = product.Id;

                try
                {
                    _importDataManager.ProductInformation.SaveProductInformation(product.Information);
                }
                catch
                {
                    _importDataManager.ProductInformation.SaveProductInformation(product.Information);
                }
            }
        }

        private void SaveProductMainPhoto(Product product)
        {
            if (product.MainPhoto != null ? !string.IsNullOrEmpty(product.MainPhoto.Url) : false)
            {
                product.MainPhoto.ProductId = product.Id;

                try
                {
                    _importDataManager.ProductMainPhotos.SaveProductMainPhoto(product.MainPhoto);
                }
                catch
                {
                    _importDataManager.ProductMainPhotos.SaveProductMainPhoto(product.MainPhoto);
                }
            }
        }

        private void SaveProductPhotos(Product product)
        {
            if (product.Photos != null ? product.Photos.Count > 0 : false)
            {
                foreach (var photo in product.Photos)
                {
                    photo.ProductId = product.Id;

                    try
                    {
                        _importDataManager.ProductPhotos.SaveProductPhoto(photo);
                    }
                    catch
                    {
                        _importDataManager.ProductPhotos.SaveProductPhoto(photo);
                    }
                }
            }
        }
        #endregion

        [HttpPost]
        [Route("~/api/product/save")]
        public IActionResult Save([FromBody] Product viewModel)
        {
            if (SaveProduct(viewModel))
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }
    }
}
