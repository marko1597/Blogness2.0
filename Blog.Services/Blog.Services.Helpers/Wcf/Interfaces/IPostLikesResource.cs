using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IPostLikesResource : IBaseResource
    {
        List<PostLike> Get(int postId);
        PostLike Add(PostLike postLike);
    }
}
