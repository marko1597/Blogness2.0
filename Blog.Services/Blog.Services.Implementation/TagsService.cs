using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class TagsService : ITag
    {
        public List<Tag> GetByPostId(int postId)
        {
            return TagsFactory.GetInstance().CreateTags().GetByPostId(postId);
        }

        public List<Tag> GetByName(string tagName)
        {
            return TagsFactory.GetInstance().CreateTags().GetTagsByName(tagName);
        }

        public bool Add(Tag tag)
        {
            return TagsFactory.GetInstance().CreateTags().Add(tag);
        }
    }
}
