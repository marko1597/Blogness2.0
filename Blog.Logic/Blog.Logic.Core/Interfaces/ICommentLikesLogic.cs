using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface ICommentLikesLogic
    {
        List<CommentLike> Get(int commentId);
        CommentLike Add(CommentLike commentLike);
    }
}
