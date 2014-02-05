using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IMediaGroupResource
    {
        List<MediaGroup> Get(Func<MediaGroup, bool> expression);
        MediaGroup Add(MediaGroup mediaGroup);
        MediaGroup Update(MediaGroup mediaGroup);
        bool Delete(MediaGroup mediaGroup);
    }
}
