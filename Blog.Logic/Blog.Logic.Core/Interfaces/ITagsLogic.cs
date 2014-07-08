using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface ITagsLogic
    {
        List<Tag> GetByPostId(int postId);
        List<Tag> GetTagsByName(string tagName);
        Tag Add(Tag tag);

    }
}
