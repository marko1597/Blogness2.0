using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    public class ImageHelperTest
    {
        private IImageHelper _imageHelper;
        private string _rootPath;

        [TestFixtureSetUp]
        public void TestInit()
        {
            _imageHelper = new ImageHelper();
            _rootPath = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
        }
        
        [Test]
        public void ShouldTransformImageToByteArray()
        {
            var image = Image.FromFile(_rootPath + @"\TestImages\Jpg_Image.jpg");
            var result = _imageHelper.ImageToByteArray(image);

            Assert.IsInstanceOf(typeof(byte[]), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenTransformImageToByteArrayHasNullAsImage()
        {
            Assert.Throws<BlogException>(() => _imageHelper.ImageToByteArray(null));
        }

        [Test]
        public void ShouldTransformByteArrayToImage()
        {
            var image = Image.FromFile(_rootPath + @"\TestImages\Jpg_Image.jpg");
            var byteArray = _imageHelper.ImageToByteArray(image);
            var result = _imageHelper.ByteArrayToImage(byteArray);

            Assert.IsInstanceOf(typeof(Image), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenTransformByteArrayToImageHasNullArray()
        {
            Assert.Throws<BlogException>(() => _imageHelper.ByteArrayToImage(null));
        }

        [Test]
        public void ShouldGenerateImagePath()
        {
            var result = _imageHelper.GenerateImagePath(1, "foo", "bar", _rootPath);
            Assert.AreEqual(_rootPath + @"\" + 1 + @"\foo\bar\", result);
        }

        [Test]
        public void ShouldGenerateImagePathCorrectlyEvenWithTrailingBackslashes()
        {
            var result = _imageHelper.GenerateImagePath(1, @"foo\", @"bar\", _rootPath + @"\");
            Assert.AreEqual(_rootPath + @"\" + 1 + @"\foo\bar\", result);
        }

        [Test]
        public void ShouldThrowExceptionSomeStringParamsAreEmptyOnGenerateImagePath()
        {
            Assert.Throws<BlogException>(() => _imageHelper.GenerateImagePath(1, "foo", "bar", string.Empty));
        }

        [Test]
        public void ShouldCreateDirectory()
        {
            var result = _imageHelper.CreateDirectory(_rootPath + @"\TestDir");

            Assert.AreEqual(true, result);
            Assert.IsTrue(Directory.Exists(_rootPath + @"\TestDir"));

            Directory.Delete(_rootPath + @"\TestDir");
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateDirectoryFails()
        {
            Assert.Throws<BlogException>(() => _imageHelper.CreateDirectory(null));
        }

        [Test]
        public void ShouldSuccessfullyDeleteDirectory()
        {
            Directory.CreateDirectory(_rootPath + @"\TestDir");
            var result = _imageHelper.DeleteDirectory(_rootPath + @"\TestDir");

            Assert.AreEqual(true, result);
            Assert.IsFalse(Directory.Exists(_rootPath + @"\TestDir"));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteDirectoryFails()
        {
            Directory.CreateDirectory(_rootPath + @"\TestDir");
            Assert.Throws<BlogException>(() => _imageHelper.CreateDirectory(null));
            Directory.Delete(_rootPath + @"\TestDir");
        }
        
        [Test]
        public void ShouldCreateImageThumbnailLargeWidth()
        {
            var result = _imageHelper.CreateThumbnail(_rootPath + @"\TestImages\Jpg_Image.jpg", _rootPath + @"\TestImages\New\", "tn_");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestImages\New\tn_Jpg_Image.jpg"));
            Assert.AreEqual(400, Image.FromFile(_rootPath + @"\TestImages\New\tn_Jpg_Image.jpg").Width);
            Assert.AreEqual(300, Image.FromFile(_rootPath + @"\TestImages\New\tn_Jpg_Image.jpg").Height);
        }

        [Test]
        public void ShouldCreateImageThumbnailSmallWidth()
        {
            var result = _imageHelper.CreateThumbnail(_rootPath + @"\TestImages\Jpg_Image_Small.jpg", _rootPath + @"\TestImages\New\", "tn_");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestImages\New\tn_Jpg_Image_Small.jpg"));
            Assert.AreEqual(394, Image.FromFile(_rootPath + @"\TestImages\New\tn_Jpg_Image_Small.jpg").Width);
            Assert.AreEqual(500, Image.FromFile(_rootPath + @"\TestImages\New\tn_Jpg_Image_Small.jpg").Height);
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateImageThumbnailFails()
        {
            Assert.Throws<BlogException>(() => _imageHelper.CreateThumbnail(null, string.Empty, "tn_"));
        }

        [Test]
        public void ShouldCreateGifThumbnail()
        {
            var result = _imageHelper.CreateGifThumbnail(_rootPath + @"\TestImages\Gif_Image.gif", _rootPath + @"\TestImages\New\", "tn_");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestImages\New\tn_Gif_Image.jpg"));
            Assert.AreEqual(400, Image.FromFile(_rootPath + @"\TestImages\New\tn_Gif_Image.jpg").Width);
            Assert.AreEqual(176, Image.FromFile(_rootPath + @"\TestImages\New\tn_Gif_Image.jpg").Height);
        }

        [Test]
        public void ShouldThrowExceptionOnCreateGifThumbnailButNonMovingImage()
        {
            var ex = Assert.Throws<BlogException>(() => _imageHelper.CreateGifThumbnail(
                _rootPath + @"\TestImages\Gif_Image_Nonmoving.gif", _rootPath + @"\TestImages\New\", "tn_"));
            Assert.That(ex.Message, Is.EqualTo("Image not animated"));
            Assert.IsFalse(File.Exists(_rootPath + @"\TestImages\New\tn_Gif_Image_Nonmoving.jpg"));
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateGifThumbnailFails()
        {
            Assert.Throws<BlogException>(() => _imageHelper.CreateGifThumbnail(null, string.Empty, "tn_"));
        }

        [Test]
        public void ShouldCreateVideoThumbnail()
        {
            var result = _imageHelper.CreateVideoThumbnail(_rootPath + @"\TestImages\Video_Image.mp4", _rootPath + @"\TestImages\New\", "tn_");

            Assert.IsTrue(result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestImages\New\tn_Video_Image.jpg"));
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateVideoThumbnailFails()
        {
            Assert.Throws<BlogException>(() => _imageHelper.CreateVideoThumbnail(null, string.Empty, "tn_"));
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            Directory.Delete(_rootPath + @"\TestImages\New", true);
        }

    }
}
