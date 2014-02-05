using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace BlogApi.Controllers
{
    public class PostPageController : ApiController
    {
        private readonly IBlogService _service;

        public PostPageController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetUserPosts")]
        public UserPosts GetUserPosts(int userId)
        {
            var userPosts = new UserPosts();
            try
            {
                userPosts = _service.GetUserPosts(userId) ?? new UserPosts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return userPosts;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetPopularPosts")]
        public List<Post> GetPopularPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = _service.GetPopularPosts(postsCount) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetRecentPosts")]
        public List<Post> GetRecentPosts(int postsCount)
        {
            var posts = new List<Post>();
            try
            {
                posts = _service.GetRecentPosts(postsCount) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }
    }
}
