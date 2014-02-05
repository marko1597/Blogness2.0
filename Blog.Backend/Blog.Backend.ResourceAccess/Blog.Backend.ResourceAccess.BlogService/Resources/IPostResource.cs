using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IPostResource
    {
        List<Post> Get(Func<Post, bool> expression);
        List<Post> Get(Func<Post, bool> expression, int threshold);
        Post Add(Post post);
        Post Update(Post post);
        bool Delete(Post post);
    }
}
