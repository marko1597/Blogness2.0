using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Logic.Mapper
{
    public static class CommentMapper
    {
        public static Comment ToDto(DataAccess.Entities.Objects.Comment comment)
        {
            if (comment != null)
            {
                var commentLikes = comment.CommentLikes != null
                    ? comment.CommentLikes.Select(CommentLikeMapper.ToDto).ToList()
                    : null;
                var comments = comment.Comments != null
                    ? comment.Comments.Select(ToDto).ToList()
                    : null;

                return new Comment
                {
                    CommentId = comment.CommentId,
                    CommentLikes = commentLikes,
                    CommentLocation = comment.CommentLocation,
                    CommentMessage = comment.CommentMessage,
                    ParentCommentId = comment.ParentCommentId,
                    PostId = comment.PostId,
                    Comments = comments,
                    User = comment.User != null ? UserMapper.ToDto(comment.User, false) : null,
                    CreatedBy = comment.CreatedBy,
                    CreatedDate = comment.CreatedDate,
                    ModifiedBy = comment.ModifiedBy,
                    ModifiedDate = comment.ModifiedDate
                };
            }
            return new Comment();
        }

        public static DataAccess.Entities.Objects.Comment ToEntity(Comment comment)
        {
            if (comment != null)
            {
                var commentLikes = comment.CommentLikes != null
                   ? comment.CommentLikes.Select(CommentLikeMapper.ToEntity).ToList()
                   : null;
                var comments = comment.Comments != null
                    ? comment.Comments.Select(ToEntity).ToList()
                    : null;

                return new DataAccess.Entities.Objects.Comment
                {
                    CommentId = comment.CommentId,
                    CommentLikes = commentLikes,
                    CommentLocation = comment.CommentLocation,
                    CommentMessage = comment.CommentMessage,
                    ParentCommentId = comment.ParentCommentId,
                    PostId = comment.PostId,
                    Comments = comments,
                    User = comment.User != null ? UserMapper.ToEntity(comment.User) : null,
                    CreatedBy = comment.CreatedBy,
                    CreatedDate = comment.CreatedDate,
                    ModifiedBy = comment.ModifiedBy,
                    ModifiedDate = comment.ModifiedDate
                };
            }
            return new DataAccess.Entities.Objects.Comment();
        }
    }
}
