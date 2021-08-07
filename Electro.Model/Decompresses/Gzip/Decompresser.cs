using System.IO;
using System.IO.Compression;

namespace Electro.Model.Decompresses.Gzip
{
    public static class Decompresser
    {
        public static byte[] Decompress(byte[] gzip)
        {
            using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                var count = 0;
                var size = 4096;
                var buffer = new byte[size];

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    do
                    {
                        count = stream.Read(buffer, 0, size);

                        if (count > 0)
                        {
                            memoryStream.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);

                    return memoryStream.ToArray();
                }
            }
        }
    }
}
