using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class TagResource : ITagResource
    {
        public List<Tag> Get(int postId)
        {
            return new DbGet().Tags(postId);
        }

        public List<Tag> Get(Func<Tag, bool> expression)
        {
            return new DbGet().Tags(expression);
        }

        public Tag Add(Tag tag)
        {
            return new DbAdd().Tag(tag);
        }

        public Tag Update(Tag tag)
        {
            return new DbUpdate().Tag(tag);
        }

        public bool Delete(Tag tag)
        {
            return new DbDelete().Tag(tag);
        }
    }
}
