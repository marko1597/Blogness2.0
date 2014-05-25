using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface ITag
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetByName(string tagName);
        Tag Add(Tag tag);
    }
}
