using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class PostLikeResource : IPostLikeResource
    {
        public List<PostLike> Get(Func<PostLike, bool> expression)
        {
            return new DbGet().PostLikes(expression);
        }

        public PostLike Add(PostLike postLike)
        {
            return new DbAdd().PostLike(postLike);
        }

        public bool Delete(PostLike postLike)
        {
            return new DbDelete().PostLike(postLike);
        }
    }
}
