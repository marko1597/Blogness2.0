using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Logic.BlogService
{
    public class PostsPageLogic
    {
        private readonly IPostResource _postResource;

        public PostsPageLogic(IPostResource postResource)
        {
            _postResource = postResource;
        }

        public UserPosts GetUserPosts(int userId)
        {
            var userPosts = new UserPosts();
            try
            {
                userPosts.User = UsersFactory.GetInstance().CreateUsers().Get(userId);
                userPosts.Posts = _postResource.Get(a => a.UserId == userId).ToList();
                userPosts.Posts.ForEach(a =>
                {
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userPosts;
        }

        public List<Post> GetPopularPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = _postResource.Get(a => a.PostId > 0, postsCount).ToList();
                posts.ForEach(a =>
                {
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        public List<Post> GetRecentPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = _postResource.Get(a => a.PostId > 0).Take(postsCount).OrderByDescending(a => a.CreatedDate).ToList();
                posts.ForEach(a =>
                {
                    a.PostContents = PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(a.PostId);
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }
    }
}
