using System.Drawing;

namespace Blog.Backend.Common.Utils
{
    public interface IImageHelper
    {
        byte[] ImageToByteArray(Image image);
        Image ByteArrayToImage(byte[] byteArray);
        string GenerateImagePath(int id, string name, string guid, string storageRoot);
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path);
        bool CreateThumbnail(string filename, string destinationPath);
        bool CreateVideoThumbnail(string filename, string destinationPath);
        bool CreateThumbnailPath(string path);
        bool DeleteThumbnailPath(string path);
    }
}
