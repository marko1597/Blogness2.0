using System.Drawing;

namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IImageHelper
    {
        byte[] ImageToByteArray(Image image);
        Image ByteArrayToImage(byte[] byteArray);
        bool SaveImage(Image image, string destinationPath, string fileName);
        bool SaveImage(byte[] bytes, string destinationPath, string fileName);
        string GenerateImagePath(int id, string guid, string storageRoot);
        Size GetComputedImageSize(int width, int height);
        Image ResizeImage(Image mg, Size newSize);
        bool CreateThumbnail(string filename, string destinationPath, string thumbnailPrefix);
        bool CreateVideoThumbnail(string filename, string destinationPath, string thumbnailPrefix);
        bool CreateGifThumbnail(string filename, string destinationPath, string thumbnailPrefix);
    }
}
