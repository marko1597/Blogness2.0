using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostsLogic : IPostsLogic
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostContentRepository _postContentRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMediaRepository _mediaRepository;

        public PostsLogic(IPostRepository postRepository, IPostContentRepository postContentRepository, ICommentRepository commentRepository, IMediaRepository mediaRepository)
        {
            _postRepository = postRepository;
            _postContentRepository = postContentRepository;
            _commentRepository = commentRepository;
            _mediaRepository = mediaRepository;
        }

        public Post GetPost(int postId)
        {
            try
            {
                var db = _postRepository.Find(a => a.PostId == postId, null, "Tags,User,PostLikes").FirstOrDefault();

                if (db == null)
                {
                    return new Post().GenerateError<Post>((int)Constants.Error.RecordNotFound,
                        string.Format("Cannot find post with Id {0}", postId));
                }

                var post = PostMapper.ToDto(db);
                if (post.User.PictureId != null)
                    post.User.Picture = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)post.User.PictureId, false).FirstOrDefault());
                if (post.User.BackgroundId != null)
                    post.User.Background = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)post.User.BackgroundId, false).FirstOrDefault());

                var dbContents = _postContentRepository.Find(a => a.PostId == postId, true).ToList();
                var postContents = new List<PostContent>();
                dbContents.ForEach(a =>
                {
                    a.Media.MediaPath = null;
                    a.Media.ThumbnailPath = null;
                    postContents.Add(PostContentMapper.ToDto(a));
                });
                post.PostContents = postContents;

                return post;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetPostsByTag(tagName).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetMorePostsByTag(string tagName, int skip)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetMorePostsByTag(tagName, 5, skip).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetPostsByUser(int userId)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetByUser(userId).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetMorePostsByUser(int userId, int skip)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetMorePostsByUser(userId, 5, skip).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetPopular(a => a.PostId > 0, postsCount).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetMorePopularPosts(int postsCount, int skip)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetMorePopularPosts(a => a.PostId > 0, postsCount, skip).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetRecent(a => a.PostId > 0, postsCount).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetMoreRecentPosts(int postsCount, int skip)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetMoreRecentPosts(a => a.PostId > 0, postsCount, skip).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a => GetPostProperties(a));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public RelatedPosts GetRelatedPosts(int postId)
        {
            try
            {
                var relatedPosts = new RelatedPosts();
                var post = GetPost(postId);

                var postByTags = new List<Post>();
                if (post.Tags == null || post.Tags.Count == 0)
                {
                    relatedPosts.PostsByTags = new List<Post>();
                }
                else
                {
                    post.Tags.ForEach(a =>
                    {
                        var tPosts = GetPostsByTag(a.TagName);
                        tPosts.ForEach(b =>
                        {
                            var canAdd = postByTags.All(p => p.Id != b.Id);
                            if (canAdd) postByTags.Add(b);
                        });
                    });
                    relatedPosts.PostsByTags = postByTags.Where(p => p.Id != postId).ToList();
                }

                var postsByUser = GetPostsByUser(post.User.Id).Where(p => p.Id != postId).ToList();
                relatedPosts.PostsByUser = postsByUser;

                return relatedPosts;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Post AddPost(Post post)
        {
            try
            {
                post.Tags = post.Tags != null ? PrepareTags(post.Tags) : null;
                post.PostContents = post.PostContents != null ? PreparePostContents(post.PostContents, post.Id) : null;

                var tPost = _postRepository.Add(PostMapper.ToEntity(post));
                return GetPost(tPost.PostId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public Post UpdatePost(Post post)
        {
            try
            {
                post.Tags = post.Tags != null ? PrepareTags(post.Tags) : null;
                post.PostContents = post.PostContents != null ? PreparePostContents(post.PostContents, post.Id) : null;

                var tPost = _postRepository.Edit(PostMapper.ToEntity(post));
                return GetPost(tPost.PostId);
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool DeletePost(int postId)
        {
            try
            {
                var db = _postRepository.Find(p => p.PostId == postId, true).FirstOrDefault();
                if (db == null) return false;

                _postRepository.Delete(db);
                return true;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        private static List<Tag> PrepareTags(IEnumerable<Tag> tags)
        {
            var enumerable = tags as Tag[] ?? tags.ToArray();
            foreach (var tag in enumerable)
            {
                tag.TagName = tag.TagName.ToLower();
            }

            return enumerable.ToList();
        }

        private static List<PostContent> PreparePostContents(IEnumerable<PostContent> contents, int postId)
        {
            var postContents = contents as PostContent[] ?? contents.ToArray();
            foreach (var postContent in postContents)
            {
                postContent.PostId = postId;
            }

            return postContents.ToList();
        }

        private Post GetPostProperties(Post post)
        {
            var contents = new List<PostContent>();
            var dbContents = _postContentRepository.Find(b => b.PostId == post.Id, true).ToList();
            dbContents.ForEach(b =>
            {
                b.Media.MediaPath = null;
                b.Media.ThumbnailPath = null;
                contents.Add(PostContentMapper.ToDto(b));
            });
            post.PostContents = contents;

            var comments = new List<Comment>();
            var dbComments = _commentRepository.GetTop(b => b.PostId == post.Id, 5).ToList();
            dbComments.ForEach(b => comments.Add(CommentMapper.ToDto(b)));
            post.Comments = comments;

            if (post.User.PictureId != null)
                post.User.Picture = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)post.User.PictureId, false).FirstOrDefault());
            if (post.User.BackgroundId != null)
                post.User.Background = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)post.User.BackgroundId, false).FirstOrDefault());

            return post;
        }
    }
}
