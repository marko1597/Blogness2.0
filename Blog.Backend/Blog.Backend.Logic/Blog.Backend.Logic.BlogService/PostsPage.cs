using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Logic.BlogService
{
    public class PostsPage
    {
        private readonly IUserResource _userResource;
        private readonly IPostResource _postResource;
        private readonly IPostContentResource _postContentResource;

        public PostsPage(IUserResource userResource, IPostResource postResource, IPostContentResource postContentResource)
        {
            _userResource = userResource;
            _postResource = postResource;
            _postContentResource = postContentResource;
        }

        public UserPosts GetUserPosts(int userId)
        {
            var userPosts = new UserPosts();
            try
            {
                userPosts.User = _userResource.Get(a => a.UserId == userId).First();
                userPosts.Posts = _postResource.Get(a => a.UserId == userId).ToList();
                userPosts.Posts.ForEach(a =>
                {
                    a.PostContents = _postContentResource.Get(b => b.PostId == a.PostId);
                    a.PostContents.ForEach(b => { b.Media = null; });
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
                posts = _postResource.Get(a => a.PostId > 0, postsCount).OrderByDescending(a => a.PostLikes.Count).ToList();
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
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }
    }
}
