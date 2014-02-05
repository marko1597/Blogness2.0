using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class PostController : ApiController
    {
        private readonly IBlogService _service;

        public PostController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetPost")]
        public Post GetPost(int postId)
        {
            var post = new Post();
            try
            {
                post = _service.GetPost(postId) ?? new Post();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetPostsByUser")]
        public List<Post> GetPostsByUser(int userId)
        {
            var posts = new List<Post>();
            try
            {
                posts = _service.GetPostsByUser(userId) ?? new List<Post>();
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetPostsByTag")]
        public List<Post> GetPostsByTag(string tagName)
        {
            var posts = new List<Post>();
            try
            {
                posts = _service.GetPostsByTag(tagName) ?? new List<Post>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return posts;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Add")]
        public Post Add(Post post)
        {
            var newPost = new Post();
            try
            {
                newPost = _service.AddPost(post);
                newPost.Comments = new List<Comment>();
                newPost.Tags = new List<Tag>();
                newPost.PostContents = new List<PostContent>();
                newPost.PostLikes = new List<PostLike>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return newPost;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("AddNew")]
        public Post AddNew(int userId)
        {
            var post = new Post();
            try
            {
                post = _service.AddPost(new Post
                {
                    PostTitle = string.Empty,
                    PostMessage = string.Empty,
                    CreatedBy = userId,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = userId,
                    ModifiedDate = DateTime.UtcNow,
                    User = _service.GetUser(userId, string.Empty)
                });

                post.Comments = new List<Comment>();
                post.Tags = new List<Tag>();
                post.PostContents = new List<PostContent>();
                post.PostLikes = new List<PostLike>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return post;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Delete")]
        public bool Delete(int postId)
        {
            try
            {
                _service.DeletePost(postId);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("Modify")]
        public bool Modify(Post post)
        {
            try
            {
                _service.ModifyPost(post);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
