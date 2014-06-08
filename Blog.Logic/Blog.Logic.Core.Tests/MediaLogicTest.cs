using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
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
        private MediaLogic _mediaLogic;
        private List<Media> _media;
        private List<Album> _albums;

        [SetUp]
        public void TestInit()
        {
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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object, 
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

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

            _mediaLogic = new MediaLogic(_mediaRepository.Object, _albumRepository.Object,
                _imageHelper.Object, _configurationHelper.Object);

            Assert.Throws<BlogException>(() => _mediaLogic.Get(1));
        }
    }
}
