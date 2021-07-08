using Electro.WebApplication.Services.ImageResizable.ImageProfiles;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Processing;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Electro.WebApplication.Services.ImageResizable
{
    public class ImageService
    {
        private readonly IEnumerable<IImageProfile> _imageProfiles;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageService(IEnumerable<IImageProfile> imageProfiles, IWebHostEnvironment webHostEnvironment)
        {
            _imageProfiles = imageProfiles;
            _webHostEnvironment = webHostEnvironment;
        }

        private void ValidateExtension(IFormFile file, IImageProfile imageProfile)
        {
            var fileExtension = Path.GetExtension(file.FileName);

            if (imageProfile.AllowedExtensions.Any(ext => ext == fileExtension.ToLower()))
            {
                return;
            }

            throw new ImageProcessingException("Неправильный формат изображения");
        }

        private void ValidateFileSize(IFormFile file, IImageProfile imageProfile)
        {
            if (file.Length > imageProfile.MaxSizeBytes)
            {
                throw new ImageProcessingException("Изображение cлишком большое");
            }
        }

        private void ValidateImageSize(Image image, IImageProfile imageProfile)
        {
            if (image.Width < imageProfile.Width || image.Height < imageProfile.Height)
            {
                throw new ImageProcessingException("Изображение слишком маленькое");
            }
        }

        private string GenerateFileName(IFormFile file)
        {
            var fileExtension = Path.GetExtension(file.FileName);
            var fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName());

            return $"{fileName}{fileExtension}";
        }

        private void Resize(Image image, IImageProfile imageProfile)
        {
            var resizeOptions = new ResizeOptions
            {
                Mode = ResizeMode.Min,
                Size = new Size(imageProfile.Width)
            };

            image.Mutate(action => action.Resize(resizeOptions));
        }

        private void Crop(Image image, IImageProfile imageProfile)
        {
            var rectangle = GetCropRectangle(image, imageProfile);

            image.Mutate(action => action.Crop(rectangle));
        }

        private Rectangle GetCropRectangle(IImageInfo image, IImageProfile imageProfile)
        {
            var widthDifference = image.Width - imageProfile.Width;
            var heightDifference = image.Height - imageProfile.Height;

            var x = widthDifference / 2;
            var y = heightDifference / 2;

            return new Rectangle(x, y, imageProfile.Width, imageProfile.Height);
        }

        public static string PathToUrl(string path)
        {
            return path?.Replace("\\", "/");
        }

        public string SaveImage(IFormFile file, ImageType imageType, string directoryPath)
        {
            var imageProfile = _imageProfiles.FirstOrDefault(profile => profile.ImageType == imageType);

            if (imageProfile == null)
            {
                throw new ImageProcessingException("Image profile has not found");
            }

            ValidateExtension(file, imageProfile);
            ValidateFileSize(file, imageProfile);

            var image = Image.Load(file.OpenReadStream());

            ValidateImageSize(image, imageProfile);

            var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, $"{imageProfile.Folder}/{directoryPath}");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = "";
            var fileName = "";

            do
            {
                fileName = GenerateFileName(file);
                filePath = Path.Combine(folderPath, fileName);
            } while (File.Exists(filePath));

            Resize(image, imageProfile);
            Crop(image, imageProfile);

            image.Save(filePath, new JpegEncoder { Quality = 100 });

            return PathToUrl(Path.Combine($"{imageProfile.Folder}/{directoryPath}", fileName));
        }
    }
}
