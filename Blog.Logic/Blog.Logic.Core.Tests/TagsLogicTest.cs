using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Blog.Common.Contracts.Utils;
using Blog.Common.Utils;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;
using Moq;
using NUnit.Framework;

namespace Blog.Logic.Core.Tests
{
    [TestFixture]
    public class TagsLogicTest
    {
        private Mock<ITagRepository> _tagRepository;
        private Mock<IPostRepository> _postRepository;
        private TagsLogic _tagsLogic;
        private List<Tag> _tags;
        private List<Post> _posts;

        [SetUp]
        public void TestInit()
        {
            #region Tags

            _tags = new List<Tag>
                    {
                        new Tag
                        {
                            TagId = 1,
                            TagName = "lorem",
                            Posts = new List<Post> {new Post {PostId = 1}, new Post {PostId = 2}},
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = 1,
                            ModifiedDate = DateTime.Now
                        },
                        new Tag
                        {
                            TagId = 2,
                            TagName = "ipsum",
                            Posts = new List<Post> {new Post {PostId = 1}, new Post {PostId = 2}},
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = 1,
                            ModifiedDate = DateTime.Now
                        },
                        new Tag
                        {
                            TagId = 3,
                            TagName = "ipsumium",
                            Posts = new List<Post> {new Post {PostId = 3}},
                            CreatedBy = 1,
                            CreatedDate = DateTime.Now,
                            ModifiedBy = 1,
                            ModifiedDate = DateTime.Now
                        }
                    };

            #endregion

            #region Posts

            _posts = new List<Post>
                     {
                         new Post
                         {
                             PostId = 1,
                             Tags = _tags.Where(a => a.TagId != 3).ToList()
                         },
                         new Post
                         {
                             PostId = 2,
                             Tags = _tags.Where(a => a.TagId != 3).ToList()
                         },
                         new Post
                         {
                             PostId = 3,
                             Tags = _tags.Where(a => a.TagId == 3).ToList()
                         },
                     };

            #endregion
        }

        [Test]
        public void ShouldGetTagsByPostId()
        {
            const int postId = 1;
            var tags = _tags.Where(a => a.TagId != 3).ToList();
            var post = _posts.Where(a => a.PostId == 1).ToList();

            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), null, string.Empty))
                .Returns(tags);

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(post);

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            var result = _tagsLogic.GetByPostId(postId);

            Assert.AreEqual(2, result.Count);

        }

        [Test]
        public void ShouldThrowExceptionWhenGetTagsByPostIdFails()
        {
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), null, string.Empty))
                .Throws(new Exception());

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);
            
            Assert.Throws<BlogException>(() => _tagsLogic.GetByPostId(1));
        }

        [Test]
        public void ShouldReturnEmptyWhenGetTagsByPostIdFoundNoPost()
        {
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), null, string.Empty))
                .Returns(new List<Tag>());

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post>());

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            var tags = _tagsLogic.GetByPostId(1);

            Assert.AreEqual(0, tags.Count);
        }

        [Test]
        public void ShouldGetTagsByName()
        {
            const string tagName = "ipsum";
            var tags = _tags.Where(a => a.TagName.Contains(tagName)).ToList();

            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _postRepository = new Mock<IPostRepository>();

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            var result = _tagsLogic.GetTagsByName(tagName);

            Assert.AreEqual(2, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetTagsByNameFails()
        {
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), null, string.Empty))
                .Throws(new Exception());

            _postRepository = new Mock<IPostRepository>();

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            Assert.Throws<BlogException>(() => _tagsLogic.GetTagsByName("foo"));
        }

        [Test]
        public void ShouldAddTag()
        {
            var tag = new Common.Contracts.Tag {TagName = "foo"};
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Add(It.IsAny<Tag>())).Returns(new Tag { TagName = "foo" });
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(),
                It.IsAny<Func<IQueryable<Tag>, IOrderedQueryable<Tag>>>(), It.IsAny<string>()))
                .Returns(new List<Tag>());

            _postRepository = new Mock<IPostRepository>();

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            var result = _tagsLogic.Add(tag);

            Assert.NotNull(result);
            Assert.IsNull(result.Error);
        }

        [Test]
        public void ShouldErrorWhenTagExistsOnAdd()
        {
            var tag = new Common.Contracts.Tag { TagName = "lorem" };
            var tags = _tags.Where(a => a.TagName == "lorem").ToList();

            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Add(It.IsAny<Tag>())).Returns(It.IsAny<Tag>);
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(),
                It.IsAny<Func<IQueryable<Tag>, IOrderedQueryable<Tag>>>(), It.IsAny<string>()))
                .Returns(tags);

            _postRepository = new Mock<IPostRepository>();

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            var result = _tagsLogic.Add(tag);

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.ValidationError, result.Error.Id);
            Assert.AreEqual("Record already exists", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddingTagFails()
        {
            var tag = new Common.Contracts.Tag { TagName = "foo" };
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), null, string.Empty))
                .Throws(new Exception());

            _postRepository = new Mock<IPostRepository>();

            _tagsLogic = new TagsLogic(_tagRepository.Object, _postRepository.Object);

            Assert.Throws<BlogException>(() => _tagsLogic.Add(tag));
        }
    }
}
