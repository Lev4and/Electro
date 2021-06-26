using System.Collections.Generic;

namespace Electro.WebApplication.Services.ImageResizable.ImageProfiles
{
    public class BoxImageProfile : IImageProfile
    {
        private const int _mb = 1048576;

        public int Width { get; set; }

        public int Height { get; set; }

        public int MaxSizeBytes { get; set; }

        public string Folder { get; set; }

        public ImageType ImageType { get; }

        public IEnumerable<string> AllowedExtensions { get; }

        public BoxImageProfile()
        {
            Width = 128;
            Height = 128;
            MaxSizeBytes = 10 * _mb;
            Folder = "upload";
            ImageType = ImageType.Box;
            AllowedExtensions = new List<string> { ".jpg", ".jpeg", ".png", ".gif" };
        }
    }
}
