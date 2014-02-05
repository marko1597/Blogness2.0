using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class PostResource : IPostResource
    {
        public List<Post> Get(Func<Post, bool> expression)
        {
            return new DbGet().Posts(expression);
        }

        public List<Post> Get(Func<Post, bool> expression, int threshold)
        {
            return new DbGet().Posts(expression, threshold);
        }

        public Post Add(Post post)
        {
            return new DbAdd().Post(post);
        }

        public Post Update(Post post)
        {
            return new DbUpdate().Post(post);
        }

        public bool Delete(Post post)
        {
            return new DbDelete().Post(post);
        }
    }
}
