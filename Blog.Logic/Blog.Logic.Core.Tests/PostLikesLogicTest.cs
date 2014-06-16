using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class PostLikesLogicTest
    {
        private Mock<IPostLikeRepository> _postLikeRepository;

        private PostLikesLogic _postLikesLogic;

        private List<PostLike> _postLikes;

        [SetUp]
        public void TestInit()
        {
            #region Post Likes

            _postLikes = new List<PostLike>
                     {
                         new PostLike
                         {
                             PostLikeId = 1,
                             PostId = 1,
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         },
                         new PostLike
                         {
                             PostLikeId = 2,
                             PostId = 1,
                             UserId = 2,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Lorem"
                                    }
                         },
                         new PostLike
                         {
                             PostLikeId = 3,
                             PostId = 2,
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetPostLikes()
        {
            var postLikes = _postLikes.Where(a => a.PostId == 1).ToList();
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), true))
                .Returns(postLikes);

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            var result = _postLikesLogic.Get(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].PostId);
            Assert.AreEqual(1, result[1].PostId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetPostLikesFoundNoRecords()
        {
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), true))
                .Returns(new List<PostLike>());

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            var result = _postLikesLogic.Get(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostLikesFails()
        {
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), true))
                .Throws(new Exception());

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            Assert.Throws<BlogException>(() => _postLikesLogic.Get(1));
        }

        [Test]
        public void ShouldAddPostLikeWhenPostNotYetLikedByUser()
        {
            var postLike = new PostLike
            {
                PostLikeId = 4,
                PostId = 1,
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "FooBar"
                }
            };
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), false))
                .Returns(new List<PostLike>());
            _postLikeRepository.Setup(a => a.Add(It.IsAny<PostLike>()))
                .Returns(postLike);

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            var result = _postLikesLogic.Add(new Common.Contracts.PostLike
            {
                PostId = 1,
                UserId = 1
            });

            Assert.NotNull(result);
            Assert.AreEqual(4, result.PostLikeId);
        }

        [Test]
        public void ShouldDeletePostLikeWhenPostLikedByUserAlready()
        {
            var postLike = _postLikes.Where(a => a.PostId == 1 && a.UserId == 1).ToList();
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), false))
                .Returns(postLike);
            _postLikeRepository.Setup(a => a.Delete(It.IsAny<PostLike>()));

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            var result = _postLikesLogic.Add(new Common.Contracts.PostLike
            {
                PostId = 1,
                UserId = 1
            });

            Assert.Null(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddPostLikeFails()
        {
            _postLikeRepository = new Mock<IPostLikeRepository>();
            _postLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostLike, bool>>>(), false))
                .Throws(new Exception());

            _postLikesLogic = new PostLikesLogic(_postLikeRepository.Object);

            Assert.Throws<BlogException>(() => _postLikesLogic.Add(new Common.Contracts.PostLike()));
        }
    }
}
