using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class TagsService : ITag
    {
        public List<Tag> GetByPostId(int postId)
        {
            return TagsFactory.GetInstance().CreateLogic().GetByPostId(postId);
        }

        public List<Tag> GetByName(string tagName)
        {
            return TagsFactory.GetInstance().CreateLogic().GetTagsByName(tagName);
        }

        public Tag Add(Tag tag)
        {
            return TagsFactory.GetInstance().CreateLogic().Add(tag);
        }
    }
}
