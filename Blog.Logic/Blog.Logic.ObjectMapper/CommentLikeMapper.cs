using Blog.Common.Contracts;
using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class CommentLikeMapper
    {
        public static CommentLike ToDto(Db.CommentLike commentLike)
        {
            return commentLike == null ? null : 
                new CommentLike
                {
                    CommentId = commentLike.CommentId,
                    CommentLikeId = commentLike.CommentLikeId,
                    UserId = commentLike.UserId,
                    CreatedBy = commentLike.CreatedBy,
                    CreatedDate = commentLike.CreatedDate,
                    ModifiedBy = commentLike.ModifiedBy,
                    ModifiedDate = commentLike.ModifiedDate
                };
        }

        public static Db.CommentLike ToEntity(CommentLike commentLike)
        {
            return commentLike == null ? null :
                new Db.CommentLike
                {
                    CommentId = commentLike.CommentId,
                    CommentLikeId = commentLike.CommentLikeId,
                    User = null,
                    UserId = commentLike.UserId,
                    CreatedBy = commentLike.CreatedBy,
                    CreatedDate = commentLike.CreatedDate,
                    ModifiedBy = commentLike.ModifiedBy,
                    ModifiedDate = commentLike.ModifiedDate
                };
        }
    }
}
