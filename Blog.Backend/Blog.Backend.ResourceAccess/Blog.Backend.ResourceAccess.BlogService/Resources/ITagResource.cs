using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface ITagResource
    {
        List<Tag> Get(int postId);
        List<Tag> Get(Func<Tag, bool> expression);
        Tag Add(Tag tag);
        Tag Update(Tag tag);
        bool Delete(Tag tag);
    }
}
