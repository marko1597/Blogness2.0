using System.Linq;
using Blog.Common.Contracts;

using Db = Blog.DataAccess.Database.Entities.Objects;

namespace Blog.Logic.ObjectMapper
{
    public static class CommentMapper
    {
        public static Comment ToDto(Db.Comment comment)
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
                    User = comment.User != null ? UserMapper.ToDto(comment.User) : null,
                    CreatedBy = comment.CreatedBy,
                    CreatedDate = comment.CreatedDate,
                    ModifiedBy = comment.ModifiedBy,
                    ModifiedDate = comment.ModifiedDate
                };
            }
            return null;
        }

        public static Db.Comment ToEntity(Comment comment)
        {
            if (comment != null)
            {
                var commentLikes = comment.CommentLikes != null
                   ? comment.CommentLikes.Select(CommentLikeMapper.ToEntity).ToList()
                   : null;
                var comments = comment.Comments != null
                    ? comment.Comments.Select(ToEntity).ToList()
                    : null;

                if (comment.User != null)
                    return new Db.Comment
                    {
                        CommentId = comment.CommentId,
                        CommentLikes = commentLikes,
                        CommentLocation = comment.CommentLocation,
                        CommentMessage = comment.CommentMessage,
                        ParentCommentId = comment.ParentCommentId,
                        PostId = comment.PostId,
                        Comments = comments,
                        User = UserMapper.ToEntity(comment.User),
                        UserId = comment.User.UserId,
                        CreatedBy = comment.CreatedBy,
                        CreatedDate = comment.CreatedDate,
                        ModifiedBy = comment.ModifiedBy,
                        ModifiedDate = comment.ModifiedDate
                    };
            }
            return null;
        }
    }
}
