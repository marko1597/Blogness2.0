using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface ICommentLikesResource : ICommentLikesService
    {
    }

    public interface ICommentLikesRestResource
    {
        List<CommentLike> Get(int commentId);
        void Add(int commentId, string username, string authenticationToken);
    }
}
