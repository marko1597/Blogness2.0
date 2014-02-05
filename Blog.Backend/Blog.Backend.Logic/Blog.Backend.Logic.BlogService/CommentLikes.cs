using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class CommentLikes
    {
        private readonly ICommentLikeResource _commentLikeResource;

        public CommentLikes(ICommentLikeResource commentLikeResource)
        {
            _commentLikeResource = commentLikeResource;
        }

        public List<CommentLike> Get(int commentId)
        {
            var commentLikes = new List<CommentLike>();
            try
            {
                commentLikes = _commentLikeResource.Get(a => a.CommentId == commentId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return commentLikes;
        }

        public void Add(CommentLike commentLike)
        {
            try
            {
                var tmpCommentLike = _commentLikeResource.Get(a => a.CommentId == commentLike.CommentId && a.UserId == commentLike.UserId);
                if (tmpCommentLike.Count > 0)
                {
                    _commentLikeResource.Delete(tmpCommentLike.FirstOrDefault());
                }
                else
                {
                    _commentLikeResource.Add(commentLike);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
