using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Contracts.Utils;
using Blog.Backend.DataAccess.Repository;
using Blog.Backend.Logic.Mapper;

namespace Blog.Backend.Logic
{
    public class CommentLikesLogic
    {
        private readonly ICommentLikeRepository _commentLikeRepository;

        public CommentLikesLogic(ICommentLikeRepository commentLikeRepository)
        {
            _commentLikeRepository = commentLikeRepository;
        }

        public List<CommentLike> Get(int commentId)
        {
            var commentLikes = new List<CommentLike>();
            try
            {
                var db = _commentLikeRepository.Find(a => a.CommentId == commentId, true).ToList();
                db.ForEach(a => commentLikes.Add(CommentLikeMapper.ToDto(a)));
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
            return commentLikes;
        }

        public void Add(CommentLike commentLike)
        {
            try
            {
                var tmpCommentLike = _commentLikeRepository.Find(a => a.CommentId == commentLike.CommentId && a.UserId == commentLike.UserId, false);
                if (tmpCommentLike.Count > 0)
                {
                    _commentLikeRepository.Delete(tmpCommentLike.FirstOrDefault());
                }
                else
                {
                    _commentLikeRepository.Add(CommentLikeMapper.ToEntity(commentLike));
                }
            }
            catch (Exception ex)
            {
                throw new BlogException(ex.Message, ex.InnerException);
            }
        }
    }
}
