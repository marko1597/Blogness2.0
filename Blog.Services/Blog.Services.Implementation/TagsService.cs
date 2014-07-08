using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class TagsService : BaseService, ITagsService
    {
        private readonly ITagsLogic _tagsLogic;

        public TagsService(ITagsLogic tagsLogic)
        {
            _tagsLogic = tagsLogic;
        }

        public List<Tag> GetByPostId(int postId)
        {
            return _tagsLogic.GetByPostId(postId);
        }

        public List<Tag> GetByName(string tagName)
        {
            return _tagsLogic.GetTagsByName(tagName);
        }

        public Tag Add(Tag tag)
        {
            return _tagsLogic.Add(tag);
        }
    }
}
