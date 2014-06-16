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
    public class PostContentsLogicTest
    {
        private Mock<IPostContentRepository> _postContentRepository;

        private PostContentsLogic _postContentsLogic;

        private List<PostContent> _postContents;

        [SetUp]
        public void TestInit()
        {
            #region Post Contents

            _postContents = new List<PostContent>
                     {
                         new PostContent
                         {
                             PostContentId = 1,
                             PostContentTitle = "Foo",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 1
                         },
                         new PostContent
                         {
                             PostContentId = 2,
                             PostContentTitle = "Bar",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 1
                         },
                         new PostContent
                         {
                             PostContentId = 3,
                             PostContentTitle = "Baz",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 2
                         }
                     };

            #endregion
        }

        [Test]
        public void ShouldGetPostContentsByPost()
        {
            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var contents = _postContentsLogic.GetByPostId(1);

            Assert.AreEqual(2, contents.Count);
            Assert.AreEqual(1, contents[0].PostId);
            Assert.AreEqual(1, contents[1].PostId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetPostContentsByPostFoundNoRecords()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(new List<PostContent>());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var contents = _postContentsLogic.GetByPostId(1);

            Assert.NotNull(contents);
            Assert.AreEqual(0, contents.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostContentsByPostFails()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Throws(new Exception());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            Assert.Throws<BlogException>(() => _postContentsLogic.GetByPostId(1));
        }

        [Test]
        public void ShouldGetPostContentById()
        {
            var postContent = _postContents.FirstOrDefault(a => a.PostContentId == 1);
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(new List<PostContent> { postContent });

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var content = _postContentsLogic.Get(1);

            Assert.AreEqual(1, content.PostContentId);
        }

        [Test]
        public void ShouldErrorWhenGetPostContentByIdFoundNoRecord()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(new List<PostContent>());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var content = _postContentsLogic.Get(1);

            Assert.NotNull(content.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, content.Error.Id);
            Assert.AreEqual("Cannot find post content with Id 1", content.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostContentByIdFails()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Throws(new Exception());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            Assert.Throws<BlogException>(() => _postContentsLogic.Get(1));
        }

        [Test]
        public void ShouldAddPostContent()
        {
            var dbResult = new PostContent
            {
                PostContentId = 4,
                PostContentTitle = "Foo",
                PostContentText = "Lorem Ipsum Dolor",
                PostId = 2
            };
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Add(It.IsAny<PostContent>())).Returns(dbResult);

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var result = _postContentsLogic.Add(new Common.Contracts.PostContent
            {
                PostContentId = 4,
                PostContentTitle = "Foo",
                PostContentText = "Lorem Ipsum Dolor",
                PostId = 5
            });

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.PostId);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddPostContentFails()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Add(It.IsAny<PostContent>())).Throws(new Exception());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            Assert.Throws<BlogException>(() => _postContentsLogic.Add(new Common.Contracts.PostContent()));
        }

        [Test]
        public void ShouldReturnTrueOnDeletePostContent()
        {
            var dbResult = new List<PostContent> { new PostContent { PostContentId = 1 } };
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), false))
               .Returns(dbResult);

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var result = _postContentsLogic.Delete(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeletePostContentFoundNoRecord()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), false))
               .Returns(new List<PostContent>());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            var result = _postContentsLogic.Delete(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeletePostContentFails()
        {
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Delete(It.IsAny<PostContent>())).Throws(new Exception());

            _postContentsLogic = new PostContentsLogic(_postContentRepository.Object);

            Assert.Throws<BlogException>(() => _postContentsLogic.Delete(1));
        }
    }
}
