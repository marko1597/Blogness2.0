using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class TagsRestResource : ITagsRestResource
    {
        public List<Tag> GetByPostId(int postId)
        {
            throw new System.NotImplementedException();
        }

        public List<Tag> GetByName(string tagName)
        {
            throw new System.NotImplementedException();
        }

        public Tag Add(Tag tag, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
