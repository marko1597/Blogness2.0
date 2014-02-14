using System;
using System.Collections.Generic;
using Blog.Backend.ResourceAccess.BlogService.Resources;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Logic.BlogService
{
    public class TagsLogic
    {
        private readonly ITagResource _tagResource;

        public TagsLogic(ITagResource tagResource)
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

        public List<Tag> GetTagsByName(string tagName)
        {
            var tags = new List<Tag>();
            try
            {
                tags = _tagResource.Get(a => a.TagName == tagName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return tags;
        }
    }
}
