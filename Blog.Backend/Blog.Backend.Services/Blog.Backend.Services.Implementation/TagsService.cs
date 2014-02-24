using System.Collections.Generic;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Logic.Factory;

namespace Blog.Backend.Services.Implementation
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
