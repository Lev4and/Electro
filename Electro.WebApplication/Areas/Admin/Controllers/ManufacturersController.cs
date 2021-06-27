using Electro.Model.Database;
using Electro.Model.Database.Entities;
using Electro.WebApplication.Models;
using Electro.WebApplication.Services;
using Electro.WebApplication.Services.ImageResizable;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace Electro.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ManufacturersController : Controller
    {
        private readonly DataManager _dataManager;
        private readonly FileService _fileService;
        private readonly ImageService _imageService;

        public ManufacturersController(DataManager dataManager, FileService fileService, ImageService imageService)
        {
            _dataManager = dataManager;
            _fileService = fileService;
            _imageService = imageService;
        }

        public IActionResult Index()
        {
            var viewModel = new ManufacturersViewModel()
            {
                Manufacturers = _dataManager.Manufacturers.GetManufacturers().ToList()
            };

            return View(viewModel);
        }

        public IActionResult Save()
        {
            var viewModel = new Manufacturer()
            {
                Logo = new ManufacturerLogo(),
                Information = new ManufacturerInformation()
            };

            return View(viewModel);
        }

        [Route("~/Admin/Manufacturers/Save/{id}")]
        public IActionResult Save(Guid id)
        {
            return View(_dataManager.Manufacturers.GetManufacturerById(id));
        }

        [HttpPost]
        public IActionResult Save(Manufacturer viewModel, IFormFile uploadedFile)
        {
            if (_dataManager.Manufacturers.SaveManufacturer(viewModel))
            {
                if (uploadedFile != null)
                {
                    viewModel.Logo.ManufacturerId = viewModel.Id;
                    viewModel.Logo.Url = _imageService.SaveImage(uploadedFile, ImageType.Box, $"manufacturers/{viewModel.Id}");

                    _dataManager.ManufacturerLogos.SaveManufacturerLogo(viewModel.Logo);
                }
                else
                {
                    if (viewModel.Logo.Id != default ? string.IsNullOrEmpty(viewModel.Logo.Url) : false)
                    {
                        _dataManager.ManufacturerLogos.DeleteManufacturerLogoById(viewModel.Logo.Id);
                    }
                }

                if (viewModel.Information.Id == default)
                {
                    if (!string.IsNullOrEmpty(viewModel.Information.Description))
                    {
                        viewModel.Information.ManufacturerId = viewModel.Id;

                        _dataManager.ManufacturerInformation.SaveManufacturerInformation(viewModel.Information);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(viewModel.Information.Description))
                    {
                        _dataManager.ManufacturerInformation.SaveManufacturerInformation(viewModel.Information);
                    }
                    else
                    {
                        _dataManager.ManufacturerInformation.DeleteManufacturerInformationById(viewModel.Information.Id);
                    }
                }

                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("Manufacturer.Name", "Производитель с таким именем уже существует");
            }

            return View(viewModel);
        }

        [Route("~/Admin/Manufacturers/Delete/{id}")]
        public IActionResult Delete(Guid id)
        {
            _fileService.DeleteDirectory($"manufacturers/{id}");
            _dataManager.Manufacturers.DeleteManufacturerById(id);

            return RedirectToAction("Index");
        }
    }
}
