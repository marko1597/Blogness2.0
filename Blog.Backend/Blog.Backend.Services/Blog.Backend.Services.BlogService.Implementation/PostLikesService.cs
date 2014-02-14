using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
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
