using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
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

        [SetUp]
        public void TestInit()
        {
            _imageHelper = new ImageHelper();
        }

        [Test]
        public void ShouldTransformImageToByteArray()
        {
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            var image = Image.FromFile(path + @"\TestImages\Jpg_Image.jpg");
            
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
            var path = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            var image = Image.FromFile(path + @"\TestImages\Jpg_Image.jpg");
            var byteArray = _imageHelper.ImageToByteArray(image);

            var result = _imageHelper.ByteArrayToImage(byteArray);

            Assert.IsInstanceOf(typeof(Image), result);
        }

        [Test]
        public void ShouldThrowExceptionWhenTransformByteArrayToImageHasNullArray()
        {
            Assert.Throws<BlogException>(() => _imageHelper.ByteArrayToImage(null));
        }
    }
}
