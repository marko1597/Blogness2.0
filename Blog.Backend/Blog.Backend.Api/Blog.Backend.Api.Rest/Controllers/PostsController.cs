using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class PostsController : ApiController
    {
        private readonly IPosts _postsSvc;
        private readonly IPostsPage _postsPageSvc;

        public PostsController(IPosts postsSvc, IPostsPage postsPageSvc)
        {
            _postsSvc = postsSvc;
            _postsPageSvc = postsPageSvc;
        }

        [HttpGet]
        [Route("api/posts/{postId}")]
        public Post Get(int postId)
        {
            var post = new Post();

            try
            {
                post = _postsSvc.GetPost(postId) ?? new Post();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        [HttpGet]
        [Route("api/posts/{tagName}")]
        public List<Post> Get(string tagName)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsSvc.GetPostsByTag(tagName) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return posts;
        }

        [HttpGet]
        [Route("api/posts/popular/{postsCount}")]
        public List<Post> GetPopular(int postsCount = 20)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsPageSvc.GetPopularPosts(postsCount) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return posts;
        }

        [HttpGet]
        [Route("api/posts/recent/{postsCount}")]
        public List<Post> GetRecent(int postsCount = 20)
        {
            var posts = new List<Post>();

            try
            {
                posts = _postsPageSvc.GetRecentPosts(postsCount) ?? new List<Post>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return posts;
        }

        [HttpGet]
        [Route("api/user/{userId}/posts")]
        public UserPosts GetUserPosts(int userId)
        {
            var posts = new UserPosts();

            try
            {
                posts = _postsPageSvc.GetUserPosts(userId) ?? new UserPosts();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return posts;
        }

        public void Post([FromBody]Post post)
        {
            try
            {
                _postsSvc.AddPost(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Put([FromBody]Post post)
        {
            try
            {
                _postsSvc.UpdatePost(post);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete([FromBody]int id)
        {
            try
            {
                _postsSvc.DeletePost(id);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
