using System.Collections.Generic;

namespace Electro.WebApplication.Services.ImageResizable.ImageProfiles
{
    public class LogoImageProfile : IImageProfile
    {
        private const int _mb = 1048576;

        public int Width { get; set; }

        public int Height { get; set; }

        public int MaxSizeBytes { get; set; }

        public string Folder { get; set; }

        public ImageType ImageType { get; }

        public IEnumerable<string> AllowedExtensions { get; }

        public LogoImageProfile()
        {
            Width = 64;
            Height = 64;
            MaxSizeBytes = 5 * _mb;
            Folder = "upload";
            ImageType = ImageType.Logo;
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        }
    }
}
