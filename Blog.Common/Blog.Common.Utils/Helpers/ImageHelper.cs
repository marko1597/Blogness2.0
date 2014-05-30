using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Blog.Common.Utils.Helpers.Interfaces;
using NReco.VideoConverter;

namespace Blog.Common.Utils.Helpers
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

        public bool CreateThumbnail(string filename, string destinationPath, string thumbnailPrefix)
        {
            try
            {
                var image = Image.FromFile(filename);
                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoder = Encoder.Quality;
                var encoderParams = new EncoderParameters(1);

                var thumb = ResizeImage(image, GetComputedImageSize(image.Width, image.Height));
                encoderParams.Param[0] = new EncoderParameter(encoder, 100L);
                thumb.Save(destinationPath + thumbnailPrefix + Path.GetFileName(filename), jgpEncoder, encoderParams);
                
                thumb.Dispose();
                image.Dispose();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            
        }

        public bool CreateVideoThumbnail(string filename, string destinationPath, string thumbnailPrefix)
        {
            try
            {
                new FFMpegConverter().GetVideoThumbnail(filename, thumbnailPrefix + Path.GetFileNameWithoutExtension(filename) + ".jpg");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateGifThumbnail(string filename, string destinationPath, string thumbnailPrefix)
        {
            try
            {
                var img = Image.FromFile(filename);
                var frames = img.GetFrameCount(FrameDimension.Time);
                if (frames <= 1) throw new ArgumentException("Image not animated");

                var frame = Convert.ToInt32(Math.Round(Convert.ToDouble(frames / 2), MidpointRounding.AwayFromZero));
                img.SelectActiveFrame(FrameDimension.Time, frame);

                var compressedImage = ResizeImage(img, GetComputedImageSize(img.Width, img.Height));
                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoder = Encoder.Quality;
                var encoderParams = new EncoderParameters(1);

                encoderParams.Param[0] = new EncoderParameter(encoder, 100L);
                compressedImage.Save(destinationPath + thumbnailPrefix + Path.GetFileNameWithoutExtension(filename) + ".jpg", jgpEncoder, encoderParams);

                compressedImage.Dispose();
                img.Dispose();

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

        private Size GetComputedImageSize(int width, int height)
        {
            if (width > 400)
            {
                var divisor = width/400;
                var tHeight = Math.Round(Convert.ToDouble(height/divisor), 0, MidpointRounding.AwayFromZero);
                var size = new Size
                           {
                               Height = Convert.ToInt32(tHeight),
                               Width = 400
                           };

                return size;
            }

            return new Size(width, height);
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
