using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Electro.WebApplication.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

                if (File.Exists(path))
                {
                    File.Delete(path);
                }
            }
        }

        public void DeleteDirectory(string directoryPath)
        {
            if (!string.IsNullOrEmpty(directoryPath))
            {
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, $"upload/{directoryPath}");

                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }
        }
    }
}
