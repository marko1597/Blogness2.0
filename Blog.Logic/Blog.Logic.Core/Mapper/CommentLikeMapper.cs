using Blog.Common.Contracts;

namespace Blog.Logic.Core.Mapper
{
    public static class CommentLikeMapper
    {
        public static CommentLike ToDto(DataAccess.Database.Entities.Objects.CommentLike commentLike)
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

        public static DataAccess.Database.Entities.Objects.CommentLike ToEntity(CommentLike commentLike)
        {
            return commentLike == null ?
                new DataAccess.Database.Entities.Objects.CommentLike() :
                new DataAccess.Database.Entities.Objects.CommentLike
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
