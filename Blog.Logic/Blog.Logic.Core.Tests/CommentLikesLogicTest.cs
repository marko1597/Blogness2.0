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
    public class CommentLikesLogicTest
    {
        private Mock<ICommentLikeRepository> _commentLikeRepository;

        private CommentLikesLogic _commentLikesLogic;

        private List<CommentLike> _commentLikes;

        [SetUp]
        public void TestInit()
        {
            #region Comment Likes

            _commentLikes = new List<CommentLike>
                     {
                         new CommentLike
                         {
                             CommentLikeId = 1,
                             CommentId = 1,
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "FooBar"
                                    }
                         },
                         new CommentLike
                         {
                             CommentLikeId = 2,
                             CommentId = 1,
                             UserId = 2,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Lorem"
                                    }
                         },
                         new CommentLike
                         {
                             CommentLikeId = 3,
                             CommentId = 2,
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
        public void ShouldGetCommentLikes()
        {
            var commentLikes = _commentLikes.Where(a => a.CommentId == 1).ToList();
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), true))
                .Returns(commentLikes);

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            var result = _commentLikesLogic.Get(1);

            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(1, result[0].CommentId);
            Assert.AreEqual(1, result[1].CommentId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetCommentLikesFoundNoRecords()
        {
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), true))
                .Returns(new List<CommentLike>());

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            var result = _commentLikesLogic.Get(1);

            Assert.NotNull(result);
            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommentLikesFails()
        {
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), true))
                .Throws(new Exception());

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            Assert.Throws<BlogException>(() => _commentLikesLogic.Get(1));
        }

        [Test]
        public void ShouldAddCommentLikeWhenCommentNotYetLikedByUser()
        {
            var commentLike = new CommentLike
            {
                CommentLikeId = 4,
                CommentId = 1,
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "FooBar"
                }
            };
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), false))
                .Returns(new List<CommentLike>());
            _commentLikeRepository.Setup(a => a.Add(It.IsAny<CommentLike>()))
                .Returns(commentLike);

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            var result = _commentLikesLogic.Add(new Common.Contracts.CommentLike
            {
                CommentId = 1, 
                UserId = 1
            });

            Assert.NotNull(result);
            Assert.AreEqual(4, result.CommentLikeId);
        }

        [Test]
        public void ShouldDeleteCommentLikeWhenCommentLikedByUserAlready()
        {
            var commentLike = _commentLikes.Where(a => a.CommentId == 1 && a.UserId == 1).ToList();
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), false))
                .Returns(commentLike);
            _commentLikeRepository.Setup(a => a.Delete(It.IsAny<CommentLike>()));

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            var result = _commentLikesLogic.Add(new Common.Contracts.CommentLike
            {
                CommentId = 1,
                UserId = 1
            });

            Assert.Null(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddCommentLikeFails()
        {
            _commentLikeRepository = new Mock<ICommentLikeRepository>();
            _commentLikeRepository.Setup(a => a.Find(It.IsAny<Expression<Func<CommentLike, bool>>>(), false))
                .Throws(new Exception());

            _commentLikesLogic = new CommentLikesLogic(_commentLikeRepository.Object);

            Assert.Throws<BlogException>(() => _commentLikesLogic.Add(new Common.Contracts.CommentLike()));
        }
    }
}
