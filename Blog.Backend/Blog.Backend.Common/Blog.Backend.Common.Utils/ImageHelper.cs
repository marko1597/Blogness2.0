using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Blog.Backend.Common.Utils
{
    public class ImageHelper : IImageHelper
    {
        public byte[] ImageToByteArray(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            return Image.FromStream(ms);
        }

        public string GenerateImagePath(int id, string storageRoot)
        {
            return storageRoot + id + "\\" + Guid.NewGuid() + "\\";
        }

        public bool CreateDirectory(string path)
        {
            try
            {
                var folders = path.Split('\\');
                Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2]);
                Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3]);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteDirectory(string path)
        {
            try
            {
                var folders = path.Split('\\');
                Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3], true);
                Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2], true);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public byte[] CreateThumbnail(string filename)
        {
            var image = Image.FromFile(filename);
            var thumb = image.GetThumbnailImage(256, 256, () => false, IntPtr.Zero);

            return ImageToByteArray(thumb);
        }

        public bool CreateThumbnailPath(string path)
        {
            try
            {
                var folders = path.Split('\\');
                Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3] + "\\" + folders[4]);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteThumbnailPath(string path)
        {
            try
            {
                var folders = path.Split('\\');
                Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3] + "\\" + folders[4], true);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
