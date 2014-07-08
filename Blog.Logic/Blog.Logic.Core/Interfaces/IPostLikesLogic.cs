using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IPostLikesLogic
    {
        List<PostLike> Get(int postId);
        PostLike Add(PostLike postLike);
    }
}
