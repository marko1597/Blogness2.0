using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.Common.Utils.Helpers;
using Blog.Common.Utils.Helpers.Interfaces;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class MediaLogicTest
    {
        private Mock<IMediaRepository> _mediaRepository;
        private Mock<IAlbumRepository> _albumRepository;
        private Mock<IImageHelper> _imageHelper;
        private Mock<IConfigurationHelper> _configurationHelper;
        private Mock<IFileHelper> _fileHelper;
        private MediaLogic _mediaLogic;
        private List<Media> _media;
        private List<Album> _albums;
        private string _rootPath;

        [SetUp]
        public void TestInit()
        {
            _rootPath = Path.GetDirectoryName(Uri.UnescapeDataString(new UriBuilder(Assembly.GetExecutingAssembly().CodeBase).Path));
            Directory.CreateDirectory(_rootPath + @"\AddedImages");

            #region Albums

            _albums = new List<Album>
                      {
                          new Album
                          {
                              AlbumId = 1,
                              AlbumName = "foo",
                              UserId = 1,
                              IsUserDefault = true,
                              User = new User
                                     {
                                         UserId = 1,
                                         UserName = "FooBar"
                                     }
                          },
                          new Album
                          {
                              AlbumId = 2,
                              AlbumName = "bar",
                              UserId = 1,
                              IsUserDefault = false,
                              User = new User
                                     {
                                         UserId = 1,
                                         UserName = "FooBar"
                                     }
                          },
                          new Album
                          {
                              AlbumId = 3,
                              AlbumName = "baz",
                              UserId = 2,
                              IsUserDefault = false,
                              User = new User
                                     {
                                         UserId = 1,
                                         UserName = "Bazz"
                                     }
                          }
                      };

            #endregion

            #region Media

            _media = new List<Media>
                     {
                         new Media
                         {
                             MediaId = 1,
                             MediaType = "image/gif",
                             CustomName = "foobarbaz",
                             MediaPath = _rootPath + @"\TestImages",
                             AlbumId = 1,
                             Album = _albums[0]
                         },
                         new Media
                         {
                             MediaId = 2,
                             MediaType = "image/jpg",
                             CustomName = "loremipsum",
                             AlbumId = 1,
                             Album = _albums[0]
                         },
                         new Media
                         {
                             MediaId = 3,
                             MediaType = "image/jpg",
                             CustomName = "dolorsitamet",
                             AlbumId = 2,
                             Album = _albums[1]
                         },
                         new Media
                         {
                             MediaId = 4,
                             MediaType = "image/mp4",
                             CustomName = "wigglefudge",
                             AlbumId = 3,
                             Album = _albums[2]
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetMediaByUser()
        {
            var albums = _albums.Where(a => a.UserId == 1).ToList();
            foreach (var a in albums)
            {
                a.Media = new List<Media>();
                var tMedia = _media.Where(m => m.AlbumId == a.AlbumId).ToList();
                foreach (var m in tMedia)
                {
                    a.Media.Add(m);
                }
            }

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(),
                It.IsAny<Func<IQueryable<Album>, IOrderedQueryable<Album>>>(), It.IsAny<string>()))
                .Returns(albums);

            _mediaRepository = new Mock<IMediaRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object, 
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByUser(1);

            Assert.AreEqual(3, result.Count);
            Assert.AreEqual(1, result[0].AlbumId);
            Assert.AreEqual(1, result[1].AlbumId);
            Assert.AreEqual(2, result[2].AlbumId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetMediaByUserFoundNoRecords()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(),
                It.IsAny<Func<IQueryable<Album>, IOrderedQueryable<Album>>>(), It.IsAny<string>()))
                .Returns(new List<Album>());

            _mediaRepository = new Mock<IMediaRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByUser(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMediaByUserFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(),
                It.IsAny<Func<IQueryable<Album>, IOrderedQueryable<Album>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _mediaRepository = new Mock<IMediaRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.GetByUser(1));
        }

        [Test]
        public void ShouldGetMediaByAlbum()
        {
            var media = _media.Where(a => a.AlbumId == 1).ToList();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(media);

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByAlbum(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].AlbumId);
            Assert.AreEqual(1, result[1].AlbumId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetMediaByAlbumFoundNoRecords()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(new List<Media>());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByAlbum(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMediaByAlbumFails()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.GetByAlbum(1));
        }

        [Test]
        public void ShouldGetMediaByName()
        {
            var media = _media.Where(a => a.CustomName == "wigglefudge").ToList();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(media);

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByName("wigglefudge");

            Assert.NotNull(result);
            Assert.AreEqual("wigglefudge", result.CustomName);
        }

        [Test]
        public void ShouldReturnErrorWhenGetMediaByNameFoundNoRecord()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(new List<Media>());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.GetByName("wigglefudge");

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Media with name wigglefudge not found", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMediaByNameFails()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.GetByName("wigglefudge"));
        }

        [Test]
        public void ShouldGetMediaById()
        {
            var media = _media.Where(a => a.MediaId == 1).ToList();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(media);

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Get(1);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.MediaId);
        }

        [Test]
        public void ShouldReturnErrorWhenGetMediaByIdFoundNoRecord()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Returns(new List<Media>());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Get(1);

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Media with Id 1 not found", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMediaByIdFails()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), true))
                .Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Get(1));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteMedia()
        {
            var media = _media.Where(a => a.MediaId == 1).ToList();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Returns(media);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.DeleteDirectory(It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.DeleteFile(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            
            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteMediaFoundNoRecord()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Returns(new List<Media>());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteMediaFailsOnMediaRepository()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Delete(1));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteMediaFailsOnFileHelperDeleteFile()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.DeleteFile(It.IsAny<string>())).Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            
            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Delete(1));
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteMediaFailsOnFileHelperDeleteDirectory()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
                .Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.DeleteFile(It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.DeleteDirectory(It.IsAny<string>())).Throws(new Exception());

            _albumRepository = new Mock<IAlbumRepository>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Delete(1));
        }

        [Test]
        public void ShouldAddMediaWithDefaultAlbum()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.IsUserDefault).ToList();
            var dbResult = new Media
                           {
                               MediaId = 5,
                               AlbumId = 1,
                               Album = _albums[0],
                               FileName = "foobarbaz.gif",
                               MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                               CustomName = guid,
                               MediaType = "image/gif",
                               MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                               ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                               ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
                           };

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateGifThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(new Common.Contracts.User {UserId = 1}, "default", "foobarbaz.gif",
                _rootPath + @"\TestImages\foobarbaz.gif", "image/gif");

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldAddMediaWithDifferentAlbum()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.IsUserDefault).ToList();
            var dbResult = new Media
            {
                MediaId = 5,
                AlbumId = 1,
                Album = _albums[0],
                FileName = "foobarbaz.gif",
                MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                CustomName = guid,
                MediaType = "image/gif",
                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateGifThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(new Common.Contracts.User { UserId = 1 }, "foo", "foobarbaz.gif",
                _rootPath + @"\TestImages\foobarbaz.gif", "image/gif");

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldAddMediaWithNonExistingAlbum()
        {
            var guid = Guid.NewGuid().ToString();
            var dbResult = new Media
            {
                MediaId = 5,
                AlbumId = 1,
                Album = _albums[0],
                FileName = "foobarbaz.gif",
                MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                CustomName = guid,
                MediaType = "image/gif",
                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateGifThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(new Common.Contracts.User { UserId = 1 }, "foo", "foobarbaz.gif",
                _rootPath + @"\TestImages\foobarbaz.gif", "image/gif");

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithAlbumLookup()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _fileHelper = new Mock<IFileHelper>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 }, 
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithAlbumAdd()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _fileHelper = new Mock<IFileHelper>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithGenerateImagePath()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithCreatingMediaDirectory()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithMovingMediaToProperPath()
        {
            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithConfigurationFetchOnCreatingThumbnail()
        {
            var guid = Guid.NewGuid().ToString();

            _mediaRepository = new Mock<IMediaRepository>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            
            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithCreatingThumbnail()
        {
            var guid = Guid.NewGuid().ToString();

            _mediaRepository = new Mock<IMediaRepository>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>()))
                .Returns(new Album { AlbumId = 1, AlbumName = "foo" });

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Throws(new Exception());

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Throws(new Exception());

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 },
                "foo", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaFailsWithMediaRepository()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.IsUserDefault).ToList();
            
            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>())).Throws(new Exception());

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateGifThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(new Common.Contracts.User { UserId = 1 }, 
                "default", "foobarbaz.gif", _rootPath + @"\TestImages\foobarbaz.gif", "image/gif"));
        }

        [Test]
        public void ShouldAddMediaByObject()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var image = Image.FromFile(_rootPath + @"\TestImages\Jpg_Image.jpg");
            var bytes = new ImageHelper().ImageToByteArray(image);
            var objParam = new Common.Contracts.Media
                           {
                               AlbumId = 1,
                               MediaType = "image/jpg",
                               MediaContent = bytes
                           };
            var dbResult = new Media
            {
                MediaId = 5,
                AlbumId = 1,
                Album = _albums[0],
                FileName = "foobarbaz.jpg",
                MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                CustomName = guid,
                MediaType = "image/jpg",
                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
            };
            image.Dispose();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _imageHelper.Setup(a => a.SaveImage(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(objParam, 1);

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldAddMediaByObjectWithoutMediaType()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var image = Image.FromFile(_rootPath + @"\TestImages\Jpg_Image.jpg");
            var bytes = new ImageHelper().ImageToByteArray(image);
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaContent = bytes
            };
            var dbResult = new Media
            {
                MediaId = 5,
                AlbumId = 1,
                Album = _albums[0],
                FileName = "foobarbaz.jpg",
                MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                CustomName = guid,
                MediaType = "image/jpg",
                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
            };
            image.Dispose();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _imageHelper.Setup(a => a.SaveImage(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(objParam, 1);

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldAddMediaByObjectWithAlbumNotFound()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var image = Image.FromFile(_rootPath + @"\TestImages\Jpg_Image.jpg");
            var bytes = new ImageHelper().ImageToByteArray(image);
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaContent = bytes,
                MediaType = "image/jpg"
            };
            var dbResult = new Media
            {
                MediaId = 5,
                AlbumId = 1,
                Album = _albums[0],
                FileName = "foobarbaz.jpg",
                MediaPath = _rootPath + @"\AddedImages\1\foo\" + guid,
                CustomName = guid,
                MediaType = "image/jpg",
                MediaUrl = string.Format("https://{0}/blogapi/api/media/{1}", UserHelper.GetLocalIpAddress(), guid),
                ThumbnailPath = _rootPath + @"\AddedImages\1\foo\" + guid + @"\tn",
                ThumbnailUrl = string.Format("https://{0}/blogapi/api/media/{1}/{2}", UserHelper.GetLocalIpAddress(), guid, "thumb")
            };
            image.Dispose();

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>()))
                .Returns(dbResult);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.MoveFile(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false)).Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>())).Returns(album.FirstOrDefault());

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.CreateThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _imageHelper.Setup(a => a.SaveImage(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var result = _mediaLogic.Add(objParam, 1);

            Assert.NotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaByObjectFailedToAddAlbum()
        {
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaType = "image/jpg"
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _fileHelper = new Mock<IFileHelper>();
            _imageHelper = new Mock<IImageHelper>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false)).Returns(new List<Album>());
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>())).Throws(new Exception());
            
            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(objParam, 1));
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaByObjectFailsToGenerateImagePath()
        {
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaType = "image/jpg"
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            _fileHelper = new Mock<IFileHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(string.Empty);

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var ex = Assert.Throws<BlogException>(() => _mediaLogic.Add(objParam, 1));
            Assert.AreEqual("Error generating media directory path", ex.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaByObjectFailsToCreateImageDirectory()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaType = "image/jpg"
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();
            
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(false);

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var ex = Assert.Throws<BlogException>(() => _mediaLogic.Add(objParam, 1));
            Assert.AreEqual("Error creating media directory", ex.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaByObjectFailsToSaveImage()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaType = "image/jpg"
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _configurationHelper = new Mock<IConfigurationHelper>();

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.SaveImage(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(false);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            var ex = Assert.Throws<BlogException>(() => _mediaLogic.Add(objParam, 1));
            Assert.AreEqual("Error saving media", ex.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddMediaByObjectFailsToAddMedia()
        {
            var guid = Guid.NewGuid().ToString();
            var album = _albums.Where(a => a.AlbumId == 1).ToList();
            var objParam = new Common.Contracts.Media
            {
                AlbumId = 1,
                MediaType = "image/jpeg"
            };

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Add(It.IsAny<Media>())).Throws(new Exception());

            _configurationHelper = new Mock<IConfigurationHelper>();
            _configurationHelper.Setup(a => a.GetAppSettings(It.IsAny<string>())).Returns("tn_");

            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(album);

            _imageHelper = new Mock<IImageHelper>();
            _imageHelper.Setup(a => a.GenerateImagePath(It.IsAny<int>(), It.IsAny<string>(),
                It.IsAny<string>(), It.IsAny<string>())).Returns(_rootPath + @"\AddedImages\1\foo\" + guid);
            _imageHelper.Setup(a => a.SaveImage(It.IsAny<byte[]>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);
            _imageHelper.Setup(a => a.CreateThumbnail(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(true);

            _fileHelper = new Mock<IFileHelper>();
            _fileHelper.Setup(a => a.CreateDirectory(It.IsAny<string>())).Returns(true);

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object, _fileHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Add(objParam, 1));
        }
    }
}
