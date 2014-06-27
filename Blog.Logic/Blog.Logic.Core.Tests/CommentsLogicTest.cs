using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
    [ExcludeFromCodeCoverage]
    public class CommentsLogicTest
    {
        private Mock<ICommentRepository> _commentsRepository;
        private Mock<IUserRepository> _userRepository;

        private CommentsLogic _commentsLogic;

        private List<Comment> _comments;

        [SetUp]
        public void TestInit()
        {
            #region Comments

            _comments = new List<Comment>
                     {
                         new Comment
                         {
                             CommentId = 1,
                             PostId = 1,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "Lorem"
                                    }
                         },
                         new Comment
                         {
                             CommentId = 2,
                             PostId = 1,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 2,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Ipsum"
                                    }
                         },
                         new Comment
                         {
                             CommentId = 3,
                             PostId = 1,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 3,
                             User = new User
                                    {
                                        UserId = 3,
                                        UserName = "Dolor"
                                    }
                         },
                         new Comment
                         {
                             CommentId = 4,
                             PostId = 2,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "Lorem"
                                    }
                         },
                         new Comment
                         {
                             CommentId = 5,
                             PostId = 2,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 2,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Ipsum"
                                    }
                         },
                         new Comment
                         {
                             CommentId = 6,
                             PostId = null,
                             ParentCommentId = 5,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 1,
                             User = new User
                                    {
                                        UserId = 1,
                                        UserName = "Lorem"
                                    }
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetCommentsByPost()
        {
            var expected = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Returns(expected);

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var results = _commentsLogic.GetByPostId(1);

            Assert.NotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(1, results[0].PostId);
            Assert.AreEqual(1, results[1].PostId);
            Assert.AreEqual(1, results[2].PostId);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommentsByPostFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.GetByPostId(1));
        }

        [Test]
        public void ShouldGetCommentsByUser()
        {
            var expected = _comments.Where(a => a.UserId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Returns(expected);

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var results = _commentsLogic.GetByUser(1);

            Assert.NotNull(results);
            Assert.AreEqual(3, results.Count);
            Assert.AreEqual(1, results[0].User.UserId);
            Assert.AreEqual(1, results[1].User.UserId);
            Assert.AreEqual(1, results[2].User.UserId);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommentsByUserFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.GetByUser(1));
        }

        [Test]
        public void ShouldGetTopComments()
        {
            var expected = _comments.Where(a => a.PostId == 2).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(expected);

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var results = _commentsLogic.GetTopComments(2, 5);

            Assert.NotNull(results);
            Assert.AreEqual(2, results.Count);
            Assert.AreEqual(2, results[0].PostId);
            Assert.AreEqual(2, results[1].PostId);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetTopCommentsFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>())).Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.GetTopComments(2, 5));
        }

        [Test]
        public void ShouldGetCommentReplies()
        {
            var expected = _comments.Where(a => a.ParentCommentId == 5).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Returns(expected);

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var results = _commentsLogic.GetReplies(5);

            Assert.NotNull(results);
            Assert.NotNull(results[0].ParentCommentId);
            Assert.IsNull(results[0].PostId);
            Assert.AreEqual(1, results.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetCommentReplyFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(),
                It.IsAny<Func<IQueryable<Comment>, IOrderedQueryable<Comment>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.GetReplies(5));
        }

        [Test]
        public void ShouldAddComment()
        {
            var dbResult = new Comment
            {
                CommentId = 7,
                PostId = 1,
                ParentCommentId = null,
                CommentMessage = "Lorem ipsum dolor",
                UserId = 1
            };

            var dbUser = new User {UserId = 1, UserName = "Lorem"};

            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Add(It.IsAny<Comment>())).Returns(dbResult);

            _userRepository = new Mock<IUserRepository>();
            _userRepository.Setup(a => a.Find(It.IsAny<Expression<Func<User, bool>>>(), false)).Returns(new List<User> { dbUser });

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var result = _commentsLogic.Add(new Common.Contracts.Comment
            {
                CommentId = 7,
                PostId = 1,
                ParentCommentId = null,
                CommentMessage = "Lorem ipsum dolor",
                User = new Common.Contracts.User
                {
                    UserId = 1,
                    UserName = "Lorem"
                }
            });

            Assert.IsNotNull(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddCommentFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Add(It.IsAny<Comment>())).Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.Add(new Common.Contracts.Comment()));
        }

        [Test]
        public void ShouldReturnTrueOnDeleteComment()
        {
            var dbResult = new List<Comment> { new Comment { CommentId = 1 } };
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(), false))
               .Returns(dbResult);

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var result = _commentsLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeleteCommentFoundNoRecord()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Comment, bool>>>(), false))
               .Returns(new List<Comment>());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            var result = _commentsLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeleteCommentFails()
        {
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.Delete(It.IsAny<Comment>())).Throws(new Exception());

            _commentsLogic = new CommentsLogic(_commentsRepository.Object, _userRepository.Object);

            Assert.Throws<BlogException>(() => _commentsLogic.Delete(1));
        }
    }
}
