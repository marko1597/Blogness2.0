using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Common.Contracts;
using Blog.Common.Utils.Extensions;
using Blog.DataAccess.Database.Repository.Interfaces;
using Blog.Logic.ObjectMapper;

namespace Blog.Logic.Core
{
    public class CommentsLogic
    {
        private readonly ICommentRepository _commentRepository;

        public CommentsLogic(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }

        public List<Comment> GetByPostId(int postId)
        {
            var comments = new List<Comment>();
            try
            {
                var db = _commentRepository.Find(a => a.PostId == postId, null, "ParentComment,Comments,CommentLikes").OrderByDescending(a => a.CreatedDate).ToList();
                db.ForEach(a => comments.Add(CommentMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return comments;
        }

        public List<Comment> GetByUser(int userId)
        {
            var comments = new List<Comment>();
            try
            {
                var db = _commentRepository.Find(a => a.UserId == userId, null, "ParentComment,Comments,CommentLikes").OrderByDescending(a => a.CreatedDate).ToList();
                db.ForEach(a => comments.Add(CommentMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return comments;
        }

        public List<Comment> GetTopComments(int postId, int commentsCount)
        {
            var comments = new List<Comment>();
            try
            {
                var db = _commentRepository.GetTop(a => a.PostId == postId, commentsCount).ToList();
                db.ForEach(a => comments.Add(CommentMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return comments;
        }

        public List<Comment> GetReplies(int commentId)
        {
            var comments = new List<Comment>();
            try
            {
                var db = _commentRepository.Find(a => a.ParentCommentId == commentId, null, "ParentComment,CommentLikes").OrderByDescending(a => a.CreatedDate).ToList();
                db.ForEach(a => comments.Add(CommentMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return comments;
        }

        public Comment Add(Comment comment)
        {
            try
            {
                return CommentMapper.ToDto(_commentRepository.Add(CommentMapper.ToEntity(comment)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }

        public bool Delete(int commentId)
        {
            try
            {
                var comment = _commentRepository.Find(a => a.CommentId == commentId, false).FirstOrDefault();
                if (comment != null)
                {
                    _commentRepository.Delete(comment);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
