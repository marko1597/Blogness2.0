using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public interface IMediaResource
    {
        List<Media> Get(Func<Media, bool> expression);
        Media Add(Media media);
        Media Update(Media media);
        bool Delete(Media media);
    }
}
