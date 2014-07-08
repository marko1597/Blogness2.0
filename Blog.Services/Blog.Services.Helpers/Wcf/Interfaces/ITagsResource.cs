using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface ITagsResource : IBaseResource
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetByName(string tagName);
        Tag Add(Tag tag);
    }
}
