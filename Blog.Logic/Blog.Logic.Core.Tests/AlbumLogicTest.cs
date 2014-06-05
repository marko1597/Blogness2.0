using System.Collections.Generic;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
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
        public void ShouldGetAlbumByUser()
        {
            
        }
    }
}
