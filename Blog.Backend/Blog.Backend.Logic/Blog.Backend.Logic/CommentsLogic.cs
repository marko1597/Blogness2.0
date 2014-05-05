using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
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
                Console.WriteLine(ex.Message);
            }
            return comments;
        }

        public bool Add(Comment comment)
        {
            try
            {
                _commentRepository.Add(CommentMapper.ToEntity(comment));
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return false;
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
                Console.WriteLine(ex.Message);
                return false;
            }
        }
    }
}
