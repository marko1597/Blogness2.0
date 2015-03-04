using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IPostLikesResource : IPostLikesService
    {
    }

    public interface IPostLikesRestResource
    {
        List<PostLike> Get(int postId);
        void Add(PostLike postLike, string authenticationToken);
    }
}
