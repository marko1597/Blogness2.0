using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class PostLikesService : IPostLikes
    {
        public List<PostLike> Get(int postId)
        {
            return PostLikesFactory.GetInstance().CreatePostLikes().Get(postId);
        }

        public PostLike Add(PostLike postLike)
        {
            return PostLikesFactory.GetInstance().CreatePostLikes().Add(postLike);
        }
    }
}
