using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IPostLikes
    {
        List<PostLike> Get(int postId);
        void Add(PostLike postLike);
    }
}
