using Blog.Backend.DataAccess.BlogService.DataAccess;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System;
using System.Collections.Generic;

namespace Blog.Backend.ResourceAccess.BlogService.Resources
{
    public class PostContentResource : IPostContentResource
    {
        public List<PostContent> Get(Func<PostContent, bool> expression)
        {
            return new DbGet().PostContent(expression);
        }

        public PostContent Add(PostContent postContent)
        {
            return new DbAdd().PostContent(postContent);
        }

        public PostContent Update(PostContent postContent)
        {
            return new DbUpdate().PostContent(postContent);
        }

        public bool Delete(PostContent postContent)
        {
            return new DbDelete().PostContent(postContent);
        }
    }
}
