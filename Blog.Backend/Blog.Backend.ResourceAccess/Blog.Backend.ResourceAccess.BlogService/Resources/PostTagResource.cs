using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class PostTagResource : IPostTagResource
    {
        public List<PostTag> Get(Func<PostTag, bool> expression)
        {
            return new DbGet().PostTags(expression);
        }
    }
}
