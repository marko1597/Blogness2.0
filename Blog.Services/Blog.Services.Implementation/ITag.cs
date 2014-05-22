using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation
{
    public interface ITag
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetByName(string tagName);
        bool Add(Tag tag);
    }
}
