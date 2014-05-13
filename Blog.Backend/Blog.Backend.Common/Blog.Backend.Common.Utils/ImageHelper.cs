using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using NReco.VideoConverter;

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

        public string GenerateImagePath(int id, string name, string guid, string storageRoot)
        {
            return storageRoot + id + "\\" + name + "\\" + guid + "\\";
        }

        public bool CreateDirectory(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
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
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool CreateThumbnail(string filename, string destinationPath)
        {
            try
            {
                var image = Image.FromFile(filename);
                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoder = Encoder.Quality;
                var encoderParams = new EncoderParameters(1);

                var thumb = ResizeImage(image, new Size(256, 256));
                encoderParams.Param[0] = new EncoderParameter(encoder, 100L);
                thumb.Save(destinationPath + Path.GetFileName(filename), jgpEncoder, encoderParams);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool CreateVideoThumbnail(string filename, string destinationPath)
        {
            try
            {
                new FFMpegConverter().GetVideoThumbnail(filename, destinationPath + Path.GetFileNameWithoutExtension(filename) + ".jpg");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateThumbnailPath(string path)
        {
            try
            {
                Directory.CreateDirectory(path);
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
                Directory.Delete(path, true);
                return true;
            }
            catch
            {
                return false;
            }
        }
        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        private Image ResizeImage(Image mg, Size newSize)
        {
            var thumbSize = new Size(newSize.Width, newSize.Height);
            var image = new Bitmap(newSize.Width, newSize.Height);
            var x = (newSize.Width - thumbSize.Width) / 2;
            var y = (newSize.Height - thumbSize.Height);

            var g = Graphics.FromImage(image);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.InterpolationMode = InterpolationMode.Low;
            g.PixelOffsetMode = PixelOffsetMode.HighSpeed;

            var rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
            g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);

            return image;
        }
    }
}
