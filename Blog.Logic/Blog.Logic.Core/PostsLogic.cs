using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostsLogic
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostContentRepository _postContentRepository;
        private readonly ITagRepository _tagRepository;
        private readonly ICommentRepository _commentRepository;
        private readonly IMediaRepository _mediaRepository;

        public PostsLogic(IPostRepository postRepository, IPostContentRepository postContentRepository, 
            ITagRepository tagRepository, ICommentRepository commentRepository, IMediaRepository mediaRepository)
        {
            _postRepository = postRepository;
            _postContentRepository = postContentRepository;
            _tagRepository = tagRepository;
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
                var dbContents = _postContentRepository.Find(a => a.PostId == postId, true).ToList();
                var postContents = new List<PostContent>();
                dbContents.ForEach(a => postContents.Add(PostContentMapper.ToDto(a)));
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
                var tag = _tagRepository.Find(a => a.TagName == tagName, true).FirstOrDefault();
                var db = _postRepository.Find(a => a.Tags.Contains(tag), null, "Tags,User,Comments,PostLikes").ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a =>
                {
                    var contents = new List<PostContent>();
                    var dbContents = _postContentRepository.Find(b => b.PostId == a.PostId, true).ToList();
                    dbContents.ForEach(b => contents.Add(PostContentMapper.ToDto(b)));
                    a.PostContents = contents;
                });
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
                var db = _postRepository.Find(a => a.UserId == userId, null, "Tags,User,Comments,PostLikes").ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a =>
                {
                    var contents = new List<PostContent>();
                    var dbContents = _postContentRepository.Find(b => b.PostId == a.PostId, true).ToList();
                    dbContents.ForEach(b => contents.Add(PostContentMapper.ToDto(b)));
                    a.PostContents = contents;
                });
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
                posts.ForEach(a =>
                {
                    var contents = new List<PostContent>();
                    var dbContents = _postContentRepository.Find(b => b.PostId == a.PostId, true).ToList();
                    dbContents.ForEach(b => contents.Add(PostContentMapper.ToDto(b)));
                    a.PostContents = contents;

                    var comments = new List<Comment>();
                    var dbComments = _commentRepository.GetTop(b => b.PostId == a.PostId, 5).ToList();
                    dbComments.ForEach(b => comments.Add(CommentMapper.ToDto(b)));
                    a.Comments = comments;

                    if (a.User.PictureId != null)
                        a.User.Picture = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.PictureId, false).FirstOrDefault());
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.BackgroundId, false).FirstOrDefault());
                });
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
                posts.ForEach(a =>
                {
                    var contents = new List<PostContent>();
                    var dbContents = _postContentRepository.Find(b => b.PostId == a.PostId, true).ToList();
                    dbContents.ForEach(b => contents.Add(PostContentMapper.ToDto(b)));
                    a.PostContents = contents;

                    var comments = new List<Comment>();
                    var dbComments = _commentRepository.GetTop(b => b.PostId == a.PostId, 5).ToList();
                    dbComments.ForEach(b => comments.Add(CommentMapper.ToDto(b)));
                    a.Comments = comments;

                    if (a.User.PictureId != null)
                        a.User.Picture = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.PictureId, false).FirstOrDefault());
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.BackgroundId, false).FirstOrDefault());
                });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public List<Post> GetMorePosts(int postsCount, int skip)
        {
            var posts = new List<Post>();
            try
            {
                var db = _postRepository.GetMorePosts(a => a.PostId > 0, postsCount, skip).ToList();
                db.ForEach(a => posts.Add(PostMapper.ToDto(a)));
                posts.ForEach(a =>
                {
                    var contents = new List<PostContent>();
                    var dbContents = _postContentRepository.Find(b => b.PostId == a.PostId, true).ToList();
                    dbContents.ForEach(b => contents.Add(PostContentMapper.ToDto(b)));
                    a.PostContents = contents;

                    var comments = new List<Comment>();
                    var dbComments = _commentRepository.GetTop(b => b.PostId == a.PostId, 5).ToList();
                    dbComments.ForEach(b => comments.Add(CommentMapper.ToDto(b)));
                    a.Comments = comments;

                    if (a.User.PictureId != null)
                        a.User.Picture = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.PictureId, false).FirstOrDefault());
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaMapper.ToDto(_mediaRepository.Find(b => b.MediaId == (int)a.User.BackgroundId, false).FirstOrDefault());
                });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }

        public Post AddPost(Post post)
        {
            try
            {
                post.Tags = post.Tags != null ? PrepareTags(post.Tags, post.User.UserId) : null;
                post.PostContents = post.PostContents != null ? PreparePostContents(post.PostContents, post.User.UserId, post.PostId) : null;
                post.CreatedBy = post.User.UserId;
                post.CreatedDate = DateTime.Now;
                post.ModifiedBy = post.User.UserId;
                post.ModifiedDate = DateTime.Now;

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
                post.Tags = post.Tags != null ? PrepareTags(post.Tags, post.User.UserId) : null;
                post.PostContents = post.PostContents != null ? PreparePostContents(post.PostContents, post.User.UserId, post.PostId) : null;
                post.ModifiedDate = DateTime.Now;

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

        private List<Tag> PrepareTags(IEnumerable<Tag> tags, int userId)
        {
            var enumerable = tags as Tag[] ?? tags.ToArray();
            foreach (var tag in enumerable)
            {
                tag.CreatedBy = userId;
                tag.CreatedDate = DateTime.Now;
                tag.ModifiedBy = userId;
                tag.ModifiedDate = DateTime.Now;
                tag.TagName = tag.TagName.ToLower();
            }

            return enumerable.ToList();
        }

        private List<PostContent> PreparePostContents(IEnumerable<PostContent> contents, int userId, int postId)
        {
            var postContents = contents as PostContent[] ?? contents.ToArray();
            foreach (var postContent in postContents)
            {
                postContent.CreatedBy = userId;
                postContent.CreatedDate = DateTime.Now;
                postContent.ModifiedBy = userId;
                postContent.ModifiedDate = DateTime.Now;
                postContent.PostId = postId;
            }

            return postContents.ToList();
        }
    }
}
