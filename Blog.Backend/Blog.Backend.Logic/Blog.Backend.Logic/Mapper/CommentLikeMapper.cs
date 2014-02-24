using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class CommentLikeMapper
    {
        public static CommentLike ToDto(DataAccess.Entities.Objects.CommentLike commentLike)
        {
            return commentLike == null ?
                new CommentLike() : 
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

        public static DataAccess.Entities.Objects.CommentLike ToEntity(CommentLike commentLike)
        {
            return commentLike == null ?
                new DataAccess.Entities.Objects.CommentLike() :
                new DataAccess.Entities.Objects.CommentLike
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
