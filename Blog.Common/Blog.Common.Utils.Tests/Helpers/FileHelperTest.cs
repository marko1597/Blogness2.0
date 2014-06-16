using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using NUnit.Framework;

namespace Blog.Common.Utils.Tests.Helpers
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class FileHelperTest
    {
        private FileHelper _fileHelper;
        private string _rootPath;

        [TestFixtureSetUp]
        public void TestInit()
        {
            _fileHelper = new FileHelper();
            _rootPath = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            
            if (Directory.Exists(_rootPath + @"\TestDir")) Directory.Delete(_rootPath + @"\TestDir", true);
            if (Directory.Exists(_rootPath + @"\TestFile")) Directory.Delete(_rootPath + @"\TestFile", true);
        }

        [Test]
        public void ShouldCreateDirectory()
        {
            var result = _fileHelper.CreateDirectory(_rootPath + @"\TestDir");

            Assert.AreEqual(true, result);
            Assert.IsTrue(Directory.Exists(_rootPath + @"\TestDir"));

            Directory.Delete(_rootPath + @"\TestDir", true);
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateDirectoryFails()
        {
            Assert.Throws<BlogException>(() => _fileHelper.CreateDirectory(null));
        }

        [Test]
        public void ShouldSuccessfullyDeleteDirectory()
        {
            if (!Directory.Exists(_rootPath + @"\TestDir")) Directory.CreateDirectory(_rootPath + @"\TestDir");
            var result = _fileHelper.DeleteDirectory(_rootPath + @"\TestDir");

            Assert.AreEqual(true, result);
            Assert.IsFalse(Directory.Exists(_rootPath + @"\TestDir"));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteDirectoryFails()
        {
            Assert.Throws<BlogException>(() => _fileHelper.DeleteDirectory(null));
        }

        [Test]
        public void ShouldCreateFile()
        {
            var result = _fileHelper.CreateFile(_rootPath + @"\TestFile\foo.txt");

            Assert.AreEqual(true, result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestFile\foo.txt"));
        }

        [Test]
        public void ShouldThrowExceptionWhenCreateFileFails()
        {
            Assert.Throws<BlogException>(() => _fileHelper.CreateFile(null));
        }

        [Test]
        public void ShouldSuccessfullyDeleteFile()
        {
            _fileHelper.CreateFile(_rootPath + @"\TestFile\baz.txt");
            var result = _fileHelper.DeleteFile(_rootPath + @"\TestFile\baz.txt");

            Assert.AreEqual(true, result);
            Assert.IsFalse(File.Exists(_rootPath + @"\TestFile\baz.txt"));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteFileFails()
        {
            _fileHelper.CreateFile(_rootPath + @"\TestFile\fish.txt");
            Assert.Throws<BlogException>(() => _fileHelper.DeleteFile(null));
            _fileHelper.DeleteDirectory(_rootPath + @"\TestFile");
        }

        [Test]
        public void ShouldMoveFile()
        {
            _fileHelper.CreateFile(_rootPath + @"\TestFile\fudge.txt");
            var result = _fileHelper.MoveFile(_rootPath + @"\TestFile\fudge.txt", _rootPath + @"\TestFile\tumbler.txt");

            Assert.AreEqual(true, result);
            Assert.IsTrue(File.Exists(_rootPath + @"\TestFile\tumbler.txt"));
            _fileHelper.DeleteFile(_rootPath + @"\TestFile\tumbler.txt");
        }

        [Test]
        public void ShouldThrowExceptionWhenMoveFileFails()
        {
            Assert.Throws<BlogException>(() => _fileHelper.MoveFile(null, null));
        }

        [TestFixtureTearDown]
        public void Dispose()
        {
            if (Directory.Exists(_rootPath + @"\TestDir")) Directory.Delete(_rootPath + @"\TestDir", true);
            if (Directory.Exists(_rootPath + @"\TestFile")) Directory.Delete(_rootPath + @"\TestFile", true);
        }
    }
}
