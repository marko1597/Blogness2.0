using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
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
