using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Implementation.Interfaces;
using Moq;
using NUnit.Framework;
using Blog.Logic.Core.Interfaces;

namespace Blog.Services.Implementation.Tests
{
    [TestFixture]
    [ExcludeFromCodeCoverage]
    public class CommentLikesServiceTest
    {
        private Mock<ICommentsLogic> _commentsLogic;
        private Mock<ICommentLikesLogic> _commentLikesLogic;
        private Mock<IRedisService> _redisService;
        private List<CommentLike> _commentLikes;

        [SetUp]
        public void Init()
        {
            #region Comment Likes

            _commentLikes = new List<CommentLike>
                     {
                         new CommentLike
                         {
                             CommentLikeId = 1,
                             CommentId = 1,
                             UserId = 1
                         },
                         new CommentLike
                         {
                             CommentLikeId = 2,
                             CommentId = 1,
                             UserId = 2
                         },
                         new CommentLike
                         {
                             CommentLikeId = 3,
                             CommentId = 2,
                             UserId = 1
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldAddCommentLikeSuccessfullyEndToEnd()
        {
            _commentsLogic = new Mock<ICommentsLogic>();
            _commentsLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(new Comment { Id = 1, PostId = 1 });

            _commentLikesLogic = new Mock<ICommentLikesLogic>();
            _commentLikesLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(_commentLikes);
            _commentLikesLogic.Setup(a => a.Add(It.IsAny<CommentLike>()))
                .Returns(new CommentLike { CommentLikeId = 1, CommentId = 1 });

            var redisService = new RedisService(new ConfigurationHelper());

            var commentLikesService = new CommentLikesService(_commentLikesLogic.Object, _commentsLogic.Object, redisService);

            Assert.DoesNotThrow(() => commentLikesService.Add(new CommentLike { CommentLikeId = 1, CommentId = 1 }));
        }

        [Test]
        public void ShouldAddCommentLikeSuccessfully()
        {
            _commentsLogic = new Mock<ICommentsLogic>();
            _commentsLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(new Comment { Id = 1, PostId = 1 });

            _commentLikesLogic = new Mock<ICommentLikesLogic>();
            _commentLikesLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(_commentLikes);
            _commentLikesLogic.Setup(a => a.Add(It.IsAny<CommentLike>()))
                .Returns(new CommentLike {CommentLikeId = 1, CommentId = 1});
            
            _redisService = new Mock<IRedisService>();
            _redisService.Setup(a => a.Publish(It.IsAny<object>()));

            var commentLikesService = new CommentLikesService(_commentLikesLogic.Object, _commentsLogic.Object, _redisService.Object);

            Assert.DoesNotThrow(() => commentLikesService.Add(new CommentLike { CommentLikeId = 1, CommentId = 1 }));
        }

        [Test]
        public void ShouldThrowErrorWhenFetchingCommentFails()
        {
            _commentsLogic = new Mock<ICommentsLogic>();
            _commentsLogic.Setup(a => a.Get(It.IsAny<int>())).Throws(new Exception());

            _commentLikesLogic = new Mock<ICommentLikesLogic>();
            _commentLikesLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(_commentLikes);
            _commentLikesLogic.Setup(a => a.Add(It.IsAny<CommentLike>()))
                .Returns(new CommentLike { CommentLikeId = 1, CommentId = 1 });

            _redisService = new Mock<IRedisService>();
            _redisService.Setup(a => a.Publish(It.IsAny<object>()));

            var commentLikesService = new CommentLikesService(_commentLikesLogic.Object, _commentsLogic.Object, _redisService.Object);
            var result = Assert.Throws<Exception>(() => commentLikesService.Add(new CommentLike { CommentLikeId = 1, CommentId = 1 }));

            Assert.IsInstanceOf(typeof(Exception), result);
        }

        [Test]
        public void ShouldThrowErrorWhenFetchingCommentLikesFails()
        {
            _commentsLogic = new Mock<ICommentsLogic>();
            _commentsLogic.Setup(a => a.Get(It.IsAny<int>())).Returns(new Comment { Id = 1, PostId = 1 });

            _commentLikesLogic = new Mock<ICommentLikesLogic>();
            _commentLikesLogic.Setup(a => a.Get(It.IsAny<int>())).Throws(new Exception());
            _commentLikesLogic.Setup(a => a.Add(It.IsAny<CommentLike>()))
                .Returns(new CommentLike { CommentLikeId = 1, CommentId = 1 });

            _redisService = new Mock<IRedisService>();
            _redisService.Setup(a => a.Publish(It.IsAny<object>()));

            var commentLikesService = new CommentLikesService(_commentLikesLogic.Object, _commentsLogic.Object, _redisService.Object);
            var result = Assert.Throws<Exception>(() => commentLikesService.Add(new CommentLike { CommentLikeId = 1, CommentId = 1 }));

            Assert.IsInstanceOf(typeof(Exception), result);
        }
    }
}
