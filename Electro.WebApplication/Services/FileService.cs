using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Electro.WebApplication.Services
{
    public class FileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public string ReadAllText(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

                if (File.Exists(path))
                {
                    return File.ReadAllText(path);
                }
            }

            return "";
        }

        public List<string> GetFiles(string directoryPath)
        {
            if (!string.IsNullOrEmpty(directoryPath))
            {
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, directoryPath);

                if (Directory.Exists(folderPath))
                {
                    return Directory.GetFiles(folderPath).ToList();
                }
            }

            return new List<string>();
        }

        public List<string> ReadAllLines(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, filePath);

                if (File.Exists(path))
                {
                    return File.ReadAllLines(path).ToList();
                }
            }

            return new List<string>();
        }

        public void WriteAllText(string filePath, string text)
        {
            if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(text))
            {
                File.WriteAllText(Path.Combine(_webHostEnvironment.WebRootPath, filePath), text);
            }
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
                var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, directoryPath);

                if (Directory.Exists(folderPath))
                {
                    Directory.Delete(folderPath, true);
                }
            }
        }
    }
}
