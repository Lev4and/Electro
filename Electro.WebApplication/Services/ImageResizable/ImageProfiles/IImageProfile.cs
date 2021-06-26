using System.Collections.Generic;

namespace Electro.WebApplication.Services.ImageResizable.ImageProfiles
{
    public interface IImageProfile
    {
        int Width { get; }

        int Height { get; }

        int MaxSizeBytes { get; }

        string Folder { get; }

        ImageType ImageType { get; }

        IEnumerable<string> AllowedExtensions { get; }
    }
}
