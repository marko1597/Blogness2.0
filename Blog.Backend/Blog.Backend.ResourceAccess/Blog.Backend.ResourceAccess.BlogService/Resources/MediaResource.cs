using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class MediaResource : IMediaResource
    {
        public List<Media> Get(Func<Media, bool> expression)
        {
            return new DbGet().Media(expression);
        }

        public Media Add(Media media)
        {
            return new DbAdd().Media(media);
        }

        public Media Update(Media media)
        {
            return new DbUpdate().Media(media);
        }

        public bool Delete(Media media)
        {
            return new DbDelete().Media(media);
        }
    }
}
