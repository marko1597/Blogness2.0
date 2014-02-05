using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;

namespace Blog.Backend.Common.BlogService
{
    public static class Utils
    {
        public static byte[] ImageToByteArray(Image image)
        {
            var ms = new MemoryStream();
            image.Save(ms, ImageFormat.Png);
            return ms.ToArray();
        }

        public static Image ByteArrayToImage(byte[] byteArray)
        {
            var ms = new MemoryStream(byteArray);
            return Image.FromStream(ms);
        }

        public static void CreateDirectory(string path)
        {
            string[] folders = path.Split('\\');
            Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2]);
            Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3]);
        }

        public static void DeleteDirectory(string path)
        {
            string[] folders = path.Split('\\');
            Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3], true);
            Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2], true);
        }

        public static byte[] CreateThumbnail(string filename)
        {
            var image = Image.FromFile(filename);
            var thumb = image.GetThumbnailImage(256, 256, () => false, IntPtr.Zero);

            return ImageToByteArray(thumb);
        }

        public static void CreateThumbnailPath(string path)
        {
            string[] folders = path.Split('\\');
            Directory.CreateDirectory(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3] + "\\" + folders[4]);
        }

        public static void DeleteThumbnailPath(string path)
        {
            string[] folders = path.Split('\\');
            Directory.Delete(folders[0] + "\\" + folders[1] + "\\" + folders[2] + "\\" + folders[3] + "\\" + folders[4], true);
        }
    }
}
