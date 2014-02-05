using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace BlogApi.Controllers
{
    public class CommentController : ApiController
    {
        private readonly IBlogService _service;

        public CommentController(IBlogService service)
        {
            _service = service;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetComments")]
        public List<Comment> GetComments(int postId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _service.GetComments(postId) ?? new List<Comment>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetCommentReplies")]
        public List<Comment> GetCommentReplies(int commentId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _service.GetCommentReplies(commentId) ?? new List<Comment>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("GetLikes")]
        public List<CommentLike> GetLikes(int commentId)
        {
            var commentLikes = new List<CommentLike>();
            try
            {
                commentLikes = _service.GetCommentLikes(commentId) ?? new List<CommentLike>();

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return commentLikes;
        }
        
        [AcceptVerbs("GET", "POST")]
        [ActionName("Add")]
        public void Add(Comment comment)
        {
            try
            {
                _service.AddComment(comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        [AcceptVerbs("GET", "POST")]
        [ActionName("LikeComment")]
        public void LikeComment(CommentLike commentLike)
        {
            try
            {
                _service.AddCommentLike(commentLike);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
