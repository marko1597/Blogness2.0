using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IPostContentResource
    {
        List<PostContent> Get(Func<PostContent, bool> expression);
        PostContent Add(PostContent postContent);
        PostContent Update(PostContent postContent);
        bool Delete(PostContent postContent);
    }
}
