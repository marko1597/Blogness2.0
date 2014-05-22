using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class PostLikesService : IPostLikes
    {
        public List<PostLike> Get(int postId)
        {
            return PostLikesFactory.GetInstance().CreatePostLikes().Get(postId);
        }

        public void Add(PostLike postLike)
        {
            PostLikesFactory.GetInstance().CreatePostLikes().Add(postLike);
        }
    }
}
