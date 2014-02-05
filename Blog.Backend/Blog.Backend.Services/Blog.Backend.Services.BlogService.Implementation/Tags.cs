using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Tags : ITag
    {
        public List<Tag> Get(int postId)
        {
            return TagsFactory.GetInstance().CreateTags().Get(postId);
        }
    }
}
