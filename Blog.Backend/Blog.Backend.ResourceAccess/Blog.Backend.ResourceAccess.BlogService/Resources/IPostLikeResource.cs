using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IPostLikeResource
    {
        List<PostLike> Get(Func<PostLike, bool> expression);
        PostLike Add(PostLike postLike);
        bool Delete(PostLike postLike);
    }
}
