using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface ITagsResource : ITagsService
    {
    }

    public interface ITagsRestResource
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetByName(string tagName);
        Tag Add(Tag tag, string authenticationToken);
    }
}
