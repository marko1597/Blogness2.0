using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers.Interfaces;
using NReco.VideoConverter;

namespace Blog.Common.Utils.Helpers
{
    public class ImageHelper : IImageHelper
    {
        private IFileHelper _fileHelper;
        public IFileHelper FileHelper
        {
            get { return _fileHelper ?? new FileHelper(); }
            set { _fileHelper = value; }
        }

        public byte[] ImageToByteArray(Image image)
        {
            try
            {
                var ms = new MemoryStream();
                image.Save(ms, ImageFormat.Png);
                return ms.ToArray();
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Image ByteArrayToImage(byte[] byteArray)
        {
            try
            {
                var ms = new MemoryStream(byteArray);
                return Image.FromStream(ms);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public string GenerateImagePath(int id, string name, string guid, string storageRoot)
        {
            try
            {
                if (string.IsNullOrEmpty(storageRoot) || string.IsNullOrEmpty(guid) || string.IsNullOrEmpty(name))
                {
                    throw new BlogException("Empty string in parameters. Provide non-empty strings.");
                }
                return storageRoot.TrimEnd('\\') + @"\" + id + @"\" + name.TrimEnd('\\') + @"\" + guid.TrimEnd('\\') + @"\";
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
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

                if (!Directory.Exists(destinationPath.TrimEnd('\\')))
                {
                    FileHelper.CreateDirectory(destinationPath);
                }

                var thumb = ResizeImage(image, GetComputedImageSize(image.Width, image.Height));
                encoderParams.Param[0] = new EncoderParameter(encoder, 100L);
                thumb.Save(destinationPath.TrimEnd('\\') + @"\" + thumbnailPrefix + Path.GetFileName(filename), jgpEncoder, encoderParams);

                thumb.Dispose();
                image.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }

        }

        public bool CreateVideoThumbnail(string filename, string destinationPath, string thumbnailPrefix)
        {
            try
            {
                if (!Directory.Exists(destinationPath.TrimEnd('\\')))
                {
                    FileHelper.CreateDirectory(destinationPath);
                }

                new FFMpegConverter().GetVideoThumbnail(filename,
                    destinationPath.TrimEnd('\\') + @"\" + thumbnailPrefix + Path.GetFileNameWithoutExtension(filename) + ".jpg");
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool CreateGifThumbnail(string filename, string destinationPath, string thumbnailPrefix)
        {
            try
            {
                var img = Image.FromFile(filename);
                var frames = img.GetFrameCount(FrameDimension.Time);
                if (frames <= 1) throw new Exception("Image not animated");

                var frame = Convert.ToInt32(Math.Round(Convert.ToDouble(frames / 2), MidpointRounding.AwayFromZero));
                img.SelectActiveFrame(FrameDimension.Time, frame);

                if (!Directory.Exists(destinationPath.TrimEnd('\\')))
                {
                    FileHelper.CreateDirectory(destinationPath);
                }

                var compressedImage = ResizeImage(img, GetComputedImageSize(img.Width, img.Height));
                var jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                var encoder = Encoder.Quality;
                var encoderParams = new EncoderParameters(1);

                encoderParams.Param[0] = new EncoderParameter(encoder, 100L);
                compressedImage.Save(destinationPath.TrimEnd('\\') + @"\" + thumbnailPrefix + Path.GetFileNameWithoutExtension(filename) + ".jpg", jgpEncoder, encoderParams);

                compressedImage.Dispose();
                img.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private ImageCodecInfo GetEncoder(ImageFormat format)
        {
            var codecs = ImageCodecInfo.GetImageDecoders();
            return codecs.FirstOrDefault(codec => codec.FormatID == format.Guid);
        }

        public Size GetComputedImageSize(int width, int height)
        {
            if (width > 400)
            {
                var divisor = Convert.ToDouble(width) / 400;
                var tHeight = Math.Round(Convert.ToDouble(height / divisor), 0, MidpointRounding.AwayFromZero);
                var size = new Size
                           {
                               Height = Convert.ToInt32(tHeight),
                               Width = 400
                           };

                return size;
            }

            return new Size(width, height);
        }

        public Image ResizeImage(Image mg, Size newSize)
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
