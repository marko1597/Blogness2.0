using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IPostLikes
    {
        List<PostLike> Get(int postId);
        void Add(PostLike postLike);
    }
}
