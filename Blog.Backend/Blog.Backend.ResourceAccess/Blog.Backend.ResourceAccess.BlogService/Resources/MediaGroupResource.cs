using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class MediaGroupResource : IMediaGroupResource
    {
        public List<MediaGroup> Get(Func<MediaGroup, bool> expression)
        {
            return new DbGet().MediaGroup(expression);
        }

        public MediaGroup Add(MediaGroup mediaGroup)
        {
            return new DbAdd().MediaGroup(mediaGroup);
        }

        public MediaGroup Update(MediaGroup mediaGroup)
        {
            return new DbUpdate().MediaGroup(mediaGroup);
        }

        public bool Delete(MediaGroup mediaGroup)
        {
            return new DbDelete().MediaGroup(mediaGroup);
        }
    }
}
