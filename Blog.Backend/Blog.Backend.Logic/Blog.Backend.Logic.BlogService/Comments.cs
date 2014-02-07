using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class Comments
    {
        private readonly ICommentResource _commentResource;
        private readonly ICommentLikeResource _commentLikeResource;
        private readonly IUserResource _userResource;

        public Comments(ICommentResource commentResource, ICommentLikeResource commentLikeResource, IUserResource userResource)
        {
            _commentResource = commentResource;
            _commentLikeResource = commentLikeResource;
            _userResource = userResource;
        }

        public List<Comment> GetByPostId(int postId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _commentResource.Get(a => a.PostId == postId, true).OrderByDescending(a => a.CreatedDate).ToList();
                comments.ForEach(a =>
                {
                    if (a.CommentLikes == null)
                    {
                        a.CommentLikes = new List<CommentLike>();
                    }
                    if (a.Comments == null)
                    {
                        a.Comments = new List<Comment>();
                    }

                    a.Comments.ForEach(b =>
                    {
                        if (b.CommentLikes == null)
                        {
                            b.CommentLikes = new List<CommentLike>();
                        }
                        if (b.Comments == null)
                        {
                            b.Comments = new List<Comment>();
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public List<Comment> GetByUser(int userId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _commentResource.Get(a => a.UserId == userId, true).OrderByDescending(a => a.CreatedDate).ToList();
                comments.ForEach(a =>
                {
                    if (a.CommentLikes == null)
                    {
                        a.CommentLikes = new List<CommentLike>();
                    }
                    if (a.Comments == null)
                    {
                        a.Comments = new List<Comment>();
                    }

                    a.Comments.ForEach(b =>
                    {
                        if (b.CommentLikes == null)
                        {
                            b.CommentLikes = new List<CommentLike>();
                        }
                        if (b.Comments == null)
                        {
                            b.Comments = new List<Comment>();
                        }
                    });
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public List<Comment> GetReplies(int commentId)
        {
            var comments = new List<Comment>();
            try
            {
                comments = _commentResource.Get(a => a.ParentCommentId == commentId).OrderByDescending(a => a.CreatedDate).ToList();
                comments.ForEach(a =>
                {
                    a.CommentLikes = _commentLikeResource.Get(x => x.CommentId == a.CommentId) ?? new List<CommentLike>();
                    a.User = _userResource.Get(b => b.UserId == a.UserId).FirstOrDefault();
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public Comment Add(Comment comment)
        {
            try
            {
                comment.UserId = comment.User.UserId;
                return _commentResource.Add(comment);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public void Delete(int commentId)
        {
            try
            {
                var comment = _commentResource.Get(a => a.CommentId == commentId).FirstOrDefault();
                if (comment != null)
                {
                    comment.UserId = comment.User.UserId;
                    _commentResource.Add(comment);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
