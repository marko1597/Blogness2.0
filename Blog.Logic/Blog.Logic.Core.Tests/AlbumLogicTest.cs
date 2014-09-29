using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class AlbumLogicTest
    {
        private Mock<IAlbumRepository> _albumRepository;

        private AlbumLogic _albumLogic;

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
                             AlbumName = "Foo",
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
                             AlbumName = "Bar",
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
                             AlbumName = "Baz",
                             UserId = 2,
                             IsUserDefault = true,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Lorem"
                                    }
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetAlbumById()
        {
            var album = _albums.Where(a => a.UserId == 1).ToList();
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Returns(album);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Get(1);

            Assert.IsNotNull(result);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldReturnErrorWhenAlbumNotFoundById()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Returns((List<Album>)null);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Get(1);

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int) Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Cannot find album with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetAlbumByIdFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.Get(1));
        }
        
        [Test]
        public void ShouldGetAlbumsByUser()
        {
            var userAlbums = _albums.Where(a => a.UserId == 1).ToList();
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Returns(userAlbums);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var albums = _albumLogic.GetByUser(1);

            Assert.AreEqual(2, albums.Count);
            Assert.AreEqual(1, albums[0].User.Id);
            Assert.AreEqual(1, albums[1].User.Id);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetAlbumsByUserFoundNoRecords()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Returns(new List<Album>());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var albums = _albumLogic.GetByUser(1);

            Assert.NotNull(albums);
            Assert.AreEqual(0, albums.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetAlbumsByUserFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), true))
                .Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.GetByUser(1));
        }

        [Test]
        public void ShouldGetUserDefaultAlbum()
        {
            var userAlbums = _albums.Where(a => a.UserId == 1 && a.IsUserDefault).ToList();
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(userAlbums);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.GetUserDefaultGroup(1);

            Assert.NotNull(result);
            Assert.AreEqual(true, result.IsUserDefault);
        }

        [Test]
        public void ShouldErrorWhenGetUserDefaultAlbumFoundNoRecord()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Returns(new List<Album>());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.GetUserDefaultGroup(1);

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Cannot find default album for user with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetUserDefaultAlbumFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
                .Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.GetUserDefaultGroup(1));
        }

        [Test]
        public void ShouldAddAlbum()
        {
            var dbResult = new Album
            {
                AlbumId = 4,
                AlbumName = "Flubber",
                UserId = 2,
                IsUserDefault = false,
                User = new User
                {
                    UserId = 2,
                    UserName = "FooBar"
                }
            };
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>())).Returns(dbResult);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Add(new Common.Contracts.Album
            {
                AlbumId = 1,
                AlbumName = "Wiggle",
                IsUserDefault = true,
                User = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                }
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.User.Id);
        }

        [Test]
        public void ShouldReturnErrorWhenAlbumNameAlreadyInUseWhenAddingAlbum()
        {
            var userAlbums = _albums.Where(a => a.UserId == 1).ToList();
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), null, null))
                .Returns(userAlbums);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Add(new Common.Contracts.Album
            {
                AlbumId = 1,
                AlbumName = "Wiggle",
                IsUserDefault = true,
                User = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int) Constants.Error.ValidationError, result.Error.Id);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddAlbumFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Add(It.IsAny<Album>())).Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.Add(new Common.Contracts.Album()));
        }

        [Test]
        public void ShouldUpdateAlbum()
        {
            var dbResult = new Album
            {
                AlbumId = 1,
                AlbumName = "Wiggle",
                UserId = 1,
                IsUserDefault = true,
                User = new User
                {
                    UserId = 1,
                    UserName = "FooBar"
                }
            };
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Edit(It.IsAny<Album>())).Returns(dbResult);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Update(new Common.Contracts.Album
            {
                AlbumId = 1,
                AlbumName = "Wiggle",
                IsUserDefault = true,
                User = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                }
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.User.Id);
        }

        [Test]
        public void ShouldReturnErrorWhenAlbumNameAlreadyInUseWhenUpdatingAlbum()
        {
            var userAlbums = _albums.Where(a => a.UserId == 1).ToList();
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), null, null))
                .Returns(userAlbums);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Update(new Common.Contracts.Album
            {
                AlbumId = 1,
                AlbumName = "Wiggle",
                IsUserDefault = true,
                User = new Common.Contracts.User
                {
                    Id = 1,
                    UserName = "FooBar"
                }
            });

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.ValidationError, result.Error.Id);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdateAlbumFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Edit(It.IsAny<Album>())).Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.Update(new Common.Contracts.Album()));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteAlbum()
        {
            var dbResult = new List<Album> { new Album { AlbumId = 1 } };
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
               .Returns(dbResult);

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteAlbumFoundNoRecord()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Album, bool>>>(), false))
               .Returns(new List<Album>());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            var result = _albumLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteAlbumFails()
        {
            _albumRepository = new Mock<IAlbumRepository>();
            _albumRepository.Setup(a => a.Delete(It.IsAny<Album>())).Throws(new Exception());

            _albumLogic = new AlbumLogic(_albumRepository.Object);

            Assert.Throws<BlogException>(() => _albumLogic.Delete(1));
        }
    }
}
