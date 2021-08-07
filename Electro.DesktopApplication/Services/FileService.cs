using System.IO;

namespace Electro.DesktopApplication.Services
{
    public class FileService
    {
        public FileService()
        {

        }

        public void DeleteFile(string filePath)
        {
            if (!string.IsNullOrEmpty(filePath))
            {
                File.Delete(filePath);
            }
        }
    }
}
