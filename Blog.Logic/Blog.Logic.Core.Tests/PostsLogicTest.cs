using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Collections.Generic;
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
    public class PostsLogicTest
    {
        private Mock<IPostRepository> _postRepository;
        private Mock<ICommentRepository> _commentsRepository;
        private Mock<IPostContentRepository> _postContentRepository;
        private Mock<IMediaRepository> _mediaRepository;
        private Mock<ITagRepository> _tagRepository;

        private List<Post> _posts;
        private List<Comment> _comments;
        private List<PostContent> _postContents;
        private List<PostLike> _postLikes;
        private List<Tag> _tags;
        private List<Media> _mediae;

        private PostsLogic _postsLogic;

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
                            Posts = new List<Post> {new Post {PostId = 1}}
                        },
                        new Tag
                        {
                            TagId = 2,
                            TagName = "ipsum",
                            Posts = new List<Post> {new Post {PostId = 1}}
                        },
                        new Tag
                        {
                            TagId = 3,
                            TagName = "dolor",
                            Posts = new List<Post> {new Post {PostId = 2}}
                        }
                    };

            #endregion

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

            #region Media

            _mediae = new List<Media>
                     {
                         new Media
                         {
                             MediaId = 1,
                             MediaType = "image/gif",
                             CustomName = "foobarbaz",
                             AlbumId = 1
                         },
                         new Media
                         {
                             MediaId = 2,
                             MediaType = "image/jpg",
                             CustomName = "loremipsum",
                             AlbumId = 1
                         },
                         new Media
                         {
                             MediaId = 3,
                             MediaType = "image/jpg",
                             CustomName = "dolorsitamet",
                             AlbumId = 2
                         },
                         new Media
                         {
                             MediaId = 4,
                             MediaType = "image/mp4",
                             CustomName = "wigglefudge",
                             AlbumId = 2
                         }
                     };

            #endregion

            #region Post Contents

            _postContents = new List<PostContent>
                     {
                         new PostContent
                         {
                             PostContentId = 1,
                             PostContentTitle = "Foo",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 1,
                             MediaId = 1,
                             Media = _mediae.FirstOrDefault(a => a.MediaId == 1)
                         },
                         new PostContent
                         {
                             PostContentId = 2,
                             PostContentTitle = "Bar",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 1,
                             MediaId = 2,
                             Media = _mediae.FirstOrDefault(a => a.MediaId == 2)
                         },
                         new PostContent
                         {
                             PostContentId = 3,
                             PostContentTitle = "Baz",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 2,
                             MediaId = 3,
                             Media = _mediae.FirstOrDefault(a => a.MediaId == 3)
                         },
                         new PostContent
                         {
                             PostContentId = 4,
                             PostContentTitle = "Fish",
                             PostContentText = "Lorem Ipsum Dolor",
                             PostId = 2,
                             MediaId = 4,
                             Media = _mediae.FirstOrDefault(a => a.MediaId == 4)
                         }
                     };

            #endregion

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
                             CommentId = 4,
                             PostId = 2,
                             ParentCommentId = null,
                             CommentMessage = "Lorem ipsum dolor",
                             UserId = 2,
                             User = new User
                                    {
                                        UserId = 2,
                                        UserName = "Ipsum"
                                    }
                         }
                     };

            #endregion

            #region Posts

            _posts = new List<Post>
            {
                new Post
                {
                    PostId = 1,
                    PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                    PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                    Comments = _comments.Where(a => a.PostId == 1).ToList(),
                    Tags = _tags.Where(a => a.TagId != 3).ToList(),
                    PostTitle = "Foo",
                    PostMessage = "Lorem Ipsum Dolor",
                    UserId = 1,
                    User = new User { UserId = 1, UserName = "Lorem" }
                },
                new Post
                {
                    PostId = 2,
                    PostLikes = _postLikes.Where(a => a.PostId == 2).ToList(),
                    PostContents = _postContents.Where(a => a.PostId == 2).ToList(),
                    Comments = _comments.Where(a => a.PostId == 2).ToList(),
                    Tags = _tags.Where(a => a.TagId == 3).ToList(),
                    PostTitle = "Foo",
                    PostMessage = "Lorem Ipsum Dolor",
                    UserId = 2,
                    User = new User { UserId = 2, UserName = "Ipsum" }
                }
            };

            #endregion
        }

        [Test]
        public void ShouldGetPostById()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPost(1);

            Assert.NotNull(result);
            Assert.AreEqual(1, result.PostId);
        }

        [Test]
        public void ShouldErrorWhenGetPostByIdFoundNoRecord()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post>());

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPost(1);

            Assert.NotNull(result.Error);
            Assert.AreEqual((int)Constants.Error.RecordNotFound, result.Error.Id);
            Assert.AreEqual("Cannot find post with Id 1", result.Error.Message);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostByIdFails()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPost(1));
        }

        [Test]
        public void ShouldGetPostsByUser()
        {
            var post = _posts.FirstOrDefault(a => a.UserId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPostsByUser(1);

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual(1, result[0].User.UserId);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetPostsByUserFoundNoRecords()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post>());

            _postContentRepository = new Mock<IPostContentRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPostsByUser(1);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostsByUserFails()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPostsByUser(1));
        }

        [Test]
        public void ShouldGetPostsByTag()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPostsByTag("lorem");

            Assert.AreEqual(1, result.Count);
            Assert.AreEqual("lorem", result[0].Tags[0].TagName.ToLower());
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetPostsByTagFoundNoRecords()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post>());

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _postContentRepository = new Mock<IPostContentRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPostsByTag("lorem");

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPostsByTagFails()
        {
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _postRepository = new Mock<IPostRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPostsByTag("lorem"));
        }

        [Test]
        public void ShouldGetRecentPosts()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem"
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetRecentPosts(5);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldFetchProfilePictureWhenGetRecentPostsUserHasNoProfilePicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = 1,
                    BackgroundId = null
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetRecentPosts(5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Picture);
        }

        [Test]
        public void ShouldFetchBackgroundPictureWhenGetRecentPostsUserHasNoBackgroundPicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = null,
                    BackgroundId = 1
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetRecentPosts(5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Background);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetRecentPostsFoundNoRecords()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post>());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetRecentPosts(5);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetRecentPostsFailsOnPostsLookup()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetRecentPosts(5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetRecentPostsFailsOnPostContentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Throws(new Exception());

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetRecentPosts(5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetRecentPostsFailsOnCommentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetRecent(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetRecentPosts(5));
        }

        [Test]
        public void ShouldGetPopularPosts()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem"
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPopularPosts(5);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldFetchProfilePictureWhenGetPopularPostsUserHasNoProfilePicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = 1,
                    BackgroundId = null
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPopularPosts(5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Picture);
        }

        [Test]
        public void ShouldFetchBackgroundPictureWhenGetPopularPostsUserHasNoBackgroundPicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = null,
                    BackgroundId = 1
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPopularPosts(5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Background);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetPopularPostsFoundNoRecords()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post>());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetPopularPosts(5);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPopularPostsFailsOnPostsLookup()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPopularPosts(5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPopularPostsFailsOnPostContentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Throws(new Exception());

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPopularPosts(5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetPopularPostsFailsOnCommentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetPopular(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetPopularPosts(5));
        }

        [Test]
        public void ShouldGetMorePosts()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem"
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetMorePosts(5, 5);

            Assert.AreEqual(1, result.Count);
        }

        [Test]
        public void ShouldFetchProfilePictureWhenGetMorePostsUserHasNoProfilePicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = 1,
                    BackgroundId = null
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetMorePosts(5, 5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Picture);
        }

        [Test]
        public void ShouldFetchBackgroundPictureWhenGetMorePostsUserHasNoBackgroundPicture()
        {
            var post = new Post
            {
                PostId = 1,
                PostLikes = _postLikes.Where(a => a.PostId == 1).ToList(),
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = _comments.Where(a => a.PostId == 1).ToList(),
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User
                {
                    UserId = 1,
                    UserName = "Lorem",
                    PictureId = null,
                    BackgroundId = 1
                }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            var tags = _tags.Where(a => a.TagName == "lorem").ToList();
            _tagRepository = new Mock<ITagRepository>();
            _tagRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Tag, bool>>>(), true))
                .Returns(tags);

            _mediaRepository = new Mock<IMediaRepository>();
            _mediaRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Media, bool>>>(), false))
               .Returns(new List<Media> { new Media() });

            var comments = _comments.Where(a => a.PostId == 1).ToList();
            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Returns(comments);

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetMorePosts(5, 5);

            Assert.AreEqual(1, result.Count);
            Assert.NotNull(result[0].User.Background);
        }

        [Test]
        public void ShouldReturnEmptyListWhenGetMorePostsFoundNoRecords()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post>());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.GetMorePosts(5, 5);

            Assert.AreEqual(0, result.Count);
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMorePostsFailsOnPostsLookup()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetMorePosts(5, 5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMorePostsFailsOnPostContentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Throws(new Exception());

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetMorePosts(5, 5));
        }

        [Test]
        public void ShouldThrowExceptionWhenGetMorePostsFailsOnCommentsLookup()
        {
            var post = _posts.FirstOrDefault(a => a.PostId == 1);
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.GetMorePosts(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns(new List<Post> { post });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();

            _commentsRepository = new Mock<ICommentRepository>();
            _commentsRepository.Setup(a => a.GetTop(It.IsAny<Expression<Func<Comment, bool>>>(), It.IsAny<int>()))
                .Throws(new Exception());

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.GetMorePosts(5, 5));
        }

        [Test]
        public void ShouldAddPost()
        {
            #region Variables

            var dbResult = new Post
                {
                    PostId = 3,
                    PostLikes = null,
                    PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                    Comments = null,
                    Tags = _tags.Where(a => a.TagId != 3).ToList(),
                    PostTitle = "Foo",
                    PostMessage = "Lorem Ipsum Dolor",
                    UserId = 1,
                    User = new User { UserId = 1, UserName = "Lorem" }
                };
            var param = new Common.Contracts.Post
            {
                PostContents = new List<Common.Contracts.PostContent>
                {
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 1,
                        PostContentTitle = "Foo",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    },
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 2,
                        PostContentTitle = "Bar",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    }
                },
                Tags = new List<Common.Contracts.Tag>
                {
                    new Common.Contracts.Tag
                    {
                        TagId = 1,
                        TagName = "lorem"
                    },
                    new Common.Contracts.Tag
                    {
                        TagId = 2,
                        TagName = "ipsum"
                    }
                },
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Add(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.AddPost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldAddPostWhenTagsNull()
        {
            #region Variables

            var dbResult = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = null,
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };
            var param = new Common.Contracts.Post
            {
                PostContents = new List<Common.Contracts.PostContent>
                {
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 1,
                        PostContentTitle = "Foo",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    },
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 2,
                        PostContentTitle = "Bar",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    }
                },
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Add(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.AddPost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldAddPostWhenPostContentsNull()
        {
            #region Variables

            var dbResult = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = null,
                Comments = null,
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };
            var param = new Common.Contracts.Post
            {
                PostContents = null,
                Tags = new List<Common.Contracts.Tag>
                {
                    new Common.Contracts.Tag
                    {
                        TagId = 1,
                        TagName = "lorem"
                    },
                    new Common.Contracts.Tag
                    {
                        TagId = 2,
                        TagName = "ipsum"
                    }
                },
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Add(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.AddPost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldThrowExceptionWhenAddPostFails()
        {
            #region Variables

            var param = new Common.Contracts.Post
            {
                PostContents = null,
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Add(It.IsAny<Post>())).Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.AddPost(param));
        }

        [Test]
        public void ShouldUpdatePost()
        {
            #region Variables

            var dbResult = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = null,
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };
            var param = new Common.Contracts.Post
            {
                PostContents = new List<Common.Contracts.PostContent>
                {
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 1,
                        PostContentTitle = "Foo",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    },
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 2,
                        PostContentTitle = "Bar",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    }
                },
                Tags = new List<Common.Contracts.Tag>
                {
                    new Common.Contracts.Tag
                    {
                        TagId = 1,
                        TagName = "lorem"
                    },
                    new Common.Contracts.Tag
                    {
                        TagId = 2,
                        TagName = "ipsum"
                    }
                },
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Edit(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.UpdatePost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldUpdatePostWhenTagsNull()
        {
            #region Variables

            var dbResult = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = null,
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };
            var param = new Common.Contracts.Post
            {
                PostContents = new List<Common.Contracts.PostContent>
                {
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 1,
                        PostContentTitle = "Foo",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    },
                    new Common.Contracts.PostContent
                    {
                        PostContentId = 2,
                        PostContentTitle = "Bar",
                        PostContentText = "Lorem Ipsum Dolor",
                        PostId = 1,
                        Media = new Common.Contracts.Media()
                    }
                },
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Edit(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.UpdatePost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldUpdatePostWhenPostContentsNull()
        {
            #region Variables

            var dbResult = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = null,
                Comments = null,
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };
            var param = new Common.Contracts.Post
            {
                PostContents = null,
                Tags = new List<Common.Contracts.Tag>
                {
                    new Common.Contracts.Tag
                    {
                        TagId = 1,
                        TagName = "lorem"
                    },
                    new Common.Contracts.Tag
                    {
                        TagId = 2,
                        TagName = "ipsum"
                    }
                },
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Edit(It.IsAny<Post>())).Returns(new Post { PostId = 3 });
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(),
                It.IsAny<Func<IQueryable<Post>, IOrderedQueryable<Post>>>(), It.IsAny<string>()))
                .Returns(new List<Post> { dbResult });

            var postContents = _postContents.Where(a => a.PostId == 1).ToList();
            _postContentRepository = new Mock<IPostContentRepository>();
            _postContentRepository.Setup(a => a.Find(It.IsAny<Expression<Func<PostContent, bool>>>(), true))
                .Returns(postContents);

            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.UpdatePost(param);

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.PostId);
        }

        [Test]
        public void ShouldThrowExceptionWhenUpdatePostFails()
        {
            #region Variables

            var param = new Common.Contracts.Post
            {
                PostContents = null,
                Tags = null,
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                User = new Common.Contracts.User { UserId = 1, UserName = "Lorem" }
            };

            #endregion

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Edit(It.IsAny<Post>())).Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.UpdatePost(param));
        }

        [Test]
        public void ShouldReturnTrueOnDeletePostContent()
        {
            var post = new Post
            {
                PostId = 3,
                PostLikes = null,
                PostContents = _postContents.Where(a => a.PostId == 1).ToList(),
                Comments = null,
                Tags = _tags.Where(a => a.TagId != 3).ToList(),
                PostTitle = "Foo",
                PostMessage = "Lorem Ipsum Dolor",
                UserId = 1,
                User = new User { UserId = 1, UserName = "Lorem" }
            };

            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Delete(It.IsAny<Post>()));
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(new List<Post> { post });

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.DeletePost(1);

            Assert.IsTrue(result);
        }

        [Test]
        public void ShouldReturnFalseWhenDeletePostContentFoundNoRecord()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Returns(new List<Post>());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            var result = _postsLogic.DeletePost(1);

            Assert.IsFalse(result);
        }

        [Test]
        public void ShouldThrowExceptionWhenDeletePostContentFails()
        {
            _postRepository = new Mock<IPostRepository>();
            _postRepository.Setup(a => a.Find(It.IsAny<Expression<Func<Post, bool>>>(), true))
                .Throws(new Exception());

            _postContentRepository = new Mock<IPostContentRepository>();
            _tagRepository = new Mock<ITagRepository>();
            _mediaRepository = new Mock<IMediaRepository>();
            _commentsRepository = new Mock<ICommentRepository>();

            _postsLogic = new PostsLogic(_postRepository.Object, _postContentRepository.Object, _tagRepository.Object,
                _commentsRepository.Object, _mediaRepository.Object);

            Assert.Throws<BlogException>(() => _postsLogic.DeletePost(1));
        }
    }
}
