using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.Model.Parsers.DNS;
using Electro.Model.Parsers.DNS.ParseWorkers;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services;
using Electro.WebApplication.Services.ImageResizable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly FileService _fileService;
        private readonly ImageService _imageService;

        public ProductsController(DataManager dataManager, FileService fileService, ImageService imageService)
        {
            _dataManager = dataManager;
            _fileService = fileService;
            _imageService = imageService;
        }

        private void DeleteUnwantedProductPhotos(ProductViewModel viewModel)
        {
            var notIncludedInTheListProductPhotos = _dataManager.ProductPhotos
                .GetNotIncludedInTheListProductPhotosByProductId(viewModel.Product.Id, (viewModel.Product.Photos
                    != null ? viewModel.Product.Photos.ToList() : new List<ProductPhoto>())).ToList();

            foreach (var productPhoto in notIncludedInTheListProductPhotos)
            {
                _fileService.DeleteFile(productPhoto.Url);
            }

            _dataManager.ProductPhotos.DeleteRangeProductPhotos(notIncludedInTheListProductPhotos);
        }

        private void ResetProductCharacteristicValueViewModel(ProductCharacteristicValueViewModel viewModel)
        {
            viewModel.UseNewValue = false;
            viewModel.CharacteristicId = Guid.Empty;
            viewModel.NewCharacteristicValue = new CharacteristicCategoryValue()
            {
                Value = null
            };
            viewModel.ProductCharacteristicValue = new ProductCharacteristicCategoryValue()
            {
                ProductId = viewModel.ProductId
            };
            viewModel.Characteristics = _dataManager.Characteristics.GetCharacteristicsByCategoryId(viewModel.CategoryId).ToList();
            viewModel.CharacteristicValues = new List<CharacteristicCategoryValue>();
        }

        private void SaveProductMainPhoto(ProductViewModel viewModel, IFormFile uploadedFile)
        {
            if (uploadedFile != null)
            {
                viewModel.Product.MainPhoto.ProductId = viewModel.Product.Id;
                viewModel.Product.MainPhoto.Url = _imageService.SaveImage(uploadedFile, ImageType.Box, $"products/{viewModel.Product.Id}/main-photo");

                _dataManager.ProductMainPhotos.SaveProductMainPhoto(viewModel.Product.MainPhoto);
            }
            else
            {
                if (viewModel.Product.MainPhoto.Id != default ? string.IsNullOrEmpty(viewModel.Product.MainPhoto.Url) : false)
                {
                    _fileService.DeleteFile(viewModel.Product.MainPhoto.Url);
                    _dataManager.ProductMainPhotos.DeleteProductMainPhotoById(viewModel.Product.MainPhoto.Id);
                }
            }
        }

        private void SaveProductPhotos(ProductViewModel viewModel, List<IFormFile> uploadedFiles)
        {
            if (uploadedFiles != null)
            {
                foreach (var file in uploadedFiles)
                {
                    var productPhoto = new ProductPhoto();

                    productPhoto.ProductId = viewModel.Product.Id;
                    productPhoto.Url = _imageService.SaveImage(file, ImageType.Box,
                        $"products/{viewModel.Product.Id}/photos");

                    _dataManager.ProductPhotos.SaveProductPhoto(productPhoto);
                }
            }
        }

        private void SaveProductInformation(ProductViewModel viewModel)
        {
            if (viewModel.Product.Information.Id == default)
            {
                if (!string.IsNullOrEmpty(viewModel.Product.Information.Description))
                {
                    viewModel.Product.Information.ProductId = viewModel.Product.Id;

                    _dataManager.ProductInformation.SaveProductInformation(viewModel.Product.Information);
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(viewModel.Product.Information.Description))
                {
                    _dataManager.ProductInformation.SaveProductInformation(viewModel.Product.Information);
                }
                else
                {
                    _dataManager.ProductInformation.DeleteProductInformationById(viewModel.Product.Information.Id);
                }
            }
        }

        public IActionResult Index()
        {
            var viewModel = new ProductsViewModel()
            {
                Products = _dataManager.Products.GetProducts().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            var viewModel = new ProductViewModel()
            {
                Product = new Product() 
                {
                    MainPhoto = new ProductMainPhoto(),
                    Information = new ProductInformation()
                },
                Categories = _dataManager.Categories.GetCategories().ToList(),
                Manufacturers = _dataManager.Manufacturers.GetManufacturers().ToList()
            };

            return View(viewModel);
        }

        [Route("~/Admin/Products/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            var viewModel = new ProductViewModel()
            {
                Product = _dataManager.Products.GetProductById(id),
                Categories = _dataManager.Categories.GetCategories().ToList(),
                Manufacturers = _dataManager.Manufacturers.GetManufacturers().ToList()
            };

            viewModel.Product.MainPhoto = viewModel.Product.MainPhoto ?? new ProductMainPhoto();
            viewModel.Product.Information = viewModel.Product.Information ?? new ProductInformation() 
            {
                Description = ""
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Save(ProductViewModel viewModel, IFormFile uploadedFile, List<IFormFile> uploadedFiles)
        {
            if (_dataManager.Products.SaveProduct(viewModel.Product))
            {
                DeleteUnwantedProductPhotos(viewModel);

                SaveProductMainPhoto(viewModel, uploadedFile);
                SaveProductPhotos(viewModel, uploadedFiles);
                SaveProductInformation(viewModel);

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Product.ManufacturerId", "Товар с такими данными уже существует");
                ModelState.AddModelError("Product.Model", "Товар с такими данными уже существует");
            }

            return View(viewModel);
        }

        [HttpGet]
        public IActionResult SaveProductCharacteristicValue(Guid productCharacteristicValueId, Guid categoryId)
        {
            var viewModel = new ProductCharacteristicValueViewModel()
            {
                UseNewValue = false,
                CharacteristicId = Guid.Empty,
                NewCharacteristicValue  = new CharacteristicCategoryValue(),
                ProductCharacteristicValue = _dataManager.ProductCharacteristicCategoryValues
                    .GetProductCharacteristicCategoryValueById(productCharacteristicValueId),
                Characteristics = _dataManager.Characteristics.GetCharacteristicsByCategoryId(categoryId).ToList(),
                CharacteristicValues = new List<CharacteristicCategoryValue>()
            };

            return PartialView("_FormProductCharacteristicValuePartial", viewModel);
        }

        [HttpPost]
        public IActionResult SaveProductCharacteristicValue(ProductCharacteristicValueViewModel viewModel)
        {
            if (viewModel.UseNewValue == true)
            {
                var characteristicCategoryValue = new CharacteristicCategoryValue()
                {
                    Id = default,
                    CharacteristicCategoryId = _dataManager.CharacteristicCategories
                        .GetCharacteristicCategoryByCharacteristicIdAndCategoryId(viewModel.CharacteristicId, 
                            viewModel.CategoryId).Id,
                    Value = viewModel.NewCharacteristicValue.Value
                };

                if (_dataManager.CharacteristicCategoryValues.SaveCharacteristicCategoryValue(characteristicCategoryValue))
                {
                    var productCharacteristicValue = new ProductCharacteristicCategoryValue()
                    {
                        Id = default,
                        ProductId = viewModel.ProductCharacteristicValue.ProductId,
                        CharacteristicCategoryValueId = characteristicCategoryValue.Id,
                    };

                    if (_dataManager.ProductCharacteristicCategoryValues
                        .SaveProductCharacteristicCategoryValue(productCharacteristicValue))
                    {
                        ResetProductCharacteristicValueViewModel(viewModel);
                    }
                    else
                    {
                        ModelState.AddModelError("CharacteristicId", "Товар содержит характеристику с данным значением");
                        ModelState.AddModelError("NewCharacteristicValue.Value", "Товар содержит характеристику с данным значением");;

                    }
                }
                else
                {
                    ModelState.AddModelError("CharacteristicId", "Значение характеристики существует с такими данными");
                    ModelState.AddModelError("NewCharacteristicValue.Value", "Значение характеристики существует с такими данными");
                }
            }
            else
            {
                if(_dataManager.ProductCharacteristicCategoryValues
                    .SaveProductCharacteristicCategoryValue(viewModel.ProductCharacteristicValue))
                {
                    ResetProductCharacteristicValueViewModel(viewModel);
                }
                else
                {
                    ModelState.AddModelError("CharacteristicId", "Товар содержит характеристику с данным значением");
                    ModelState.AddModelError("ProductCharacteristicCategoryValue.CharacteristicCategoryValueId", "Товар содержит характеристику с данным значением");
                }
            }

            return PartialView("_FormProductCharacteristicValuePartial", viewModel);
        }

        [HttpGet]
        public IActionResult DeleteProductCharacteristicValue(Guid productCharacteristicValueId)
        {
            _dataManager.ProductCharacteristicCategoryValues
                .DeleteProductCharacteristicCategoryValueById(productCharacteristicValueId);

            return Ok();
        }

        [HttpPost]
        public IActionResult CharacteristicValuesByCharacteristicId(Guid characteristicId)
        {
            var viewModel = new CharacteristicValuesViewModel()
            {
                CharacteristicValues = _dataManager.CharacteristicCategoryValues
                    .GetCharacteristicCategoryValuesByCharacteristicId(characteristicId).ToList()
            };

            return PartialView("_CharacteristicValueItemsPartial", viewModel);
        }

        [HttpPost]
        public IActionResult ProductCharacteristicValues(Guid productId)
        {
            var viewModel = new ProductCharacteristicValuesViewModel()
            {
                ProductCharacteristicValues = _dataManager.ProductCharacteristicCategoryValues
                    .GetProductCharacteristicCategoryValuesByProductId(productId).ToList()
            };

            return PartialView("_DataTableProductCharacteristicValuesPartial", viewModel);
        }

        [Route("~/Admin/Products/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _fileService.DeleteDirectory($"upload/products/{id}");
            _dataManager.Products.DeleteProductById(id);

            return RedirectToAction("Index");
        }
    }
}
