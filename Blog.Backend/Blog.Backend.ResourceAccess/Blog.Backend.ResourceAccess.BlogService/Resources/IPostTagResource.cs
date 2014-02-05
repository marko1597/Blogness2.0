using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IPostTagResource
    {
        List<PostTag> Get(Func<PostTag, bool> expression);
    }
}
