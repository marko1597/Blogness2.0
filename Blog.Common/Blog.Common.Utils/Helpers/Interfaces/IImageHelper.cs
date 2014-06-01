﻿using System.Drawing;

namespace Blog.Common.Utils.Helpers.Interfaces
{
    public interface IImageHelper
    {
        byte[] ImageToByteArray(Image image);
        Image ByteArrayToImage(byte[] byteArray);
        string GenerateImagePath(int id, string name, string guid, string storageRoot);
        Size GetComputedImageSize(int width, int height);
        Image ResizeImage(Image mg, Size newSize);
        bool CreateDirectory(string path);
        bool DeleteDirectory(string path);
        bool CreateThumbnail(string filename, string destinationPath, string thumbnailPrefix);
        bool CreateVideoThumbnail(string filename, string destinationPath, string thumbnailPrefix);
        bool CreateGifThumbnail(string filename, string destinationPath, string thumbnailPrefix);
        bool CreateThumbnailPath(string path);
        bool DeleteThumbnailPath(string path);
    }
}
