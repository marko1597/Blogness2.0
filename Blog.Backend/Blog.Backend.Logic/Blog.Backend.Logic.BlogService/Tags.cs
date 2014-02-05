using System;
using System.Collections.Generic;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class Tags
    {
        private readonly ITagResource _tagResource;

        public Tags(ITagResource tagResource)
        {
            _tagResource = tagResource;
        }

        public List<Tag> Get(int postId)
        {
            var tags = new List<Tag>();
            try
            {
                tags = _tagResource.Get(postId);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tags;
        }
    }
}
