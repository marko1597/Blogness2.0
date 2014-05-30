using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.Core.Factory;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class PostsPageLogic
    {
        private readonly IPostRepository _postRepository;

        public PostsPageLogic(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public UserPosts GetUserPosts(int userId)
        {
            var userPosts = new UserPosts();
            try
            {
                userPosts.User = UsersFactory.GetInstance().CreateLogic().Get(userId);
                var dbPosts = _postRepository.Find(a => a.UserId == userId, null, "PostContents,Tags,User,Comments,PostLikes").ToList();
                dbPosts.ForEach(a => userPosts.Posts.Add(PostMapper.ToDto(a)));
                userPosts.Posts.ForEach(a =>
                {
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return userPosts;
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
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                    a.Comments = CommentsFactory.GetInstance().CreateComments().GetTopComments(a.PostId, 5);
                    if (a.User.PictureId != null)
                        a.User.Picture = MediaFactory.GetInstance().CreateMedia().Get((int) a.User.PictureId);
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaFactory.GetInstance().CreateMedia().Get((int)a.User.BackgroundId);
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
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                    a.Comments = CommentsFactory.GetInstance().CreateComments().GetTopComments(a.PostId, 5);
                    if (a.User.PictureId != null)
                        a.User.Picture = MediaFactory.GetInstance().CreateMedia().Get((int)a.User.PictureId);
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaFactory.GetInstance().CreateMedia().Get((int)a.User.BackgroundId);
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
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                    a.Comments = CommentsFactory.GetInstance().CreateComments().GetTopComments(a.PostId, 5);
                    if (a.User.PictureId != null)
                        a.User.Picture = MediaFactory.GetInstance().CreateMedia().Get((int)a.User.PictureId);
                    if (a.User.BackgroundId != null)
                        a.User.Background = MediaFactory.GetInstance().CreateMedia().Get((int)a.User.BackgroundId);
                });
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return posts;
        }
    }
}
