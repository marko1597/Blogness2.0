using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface ITag
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetByName(string tagName);
        bool Add(Tag tag);
    }
}
