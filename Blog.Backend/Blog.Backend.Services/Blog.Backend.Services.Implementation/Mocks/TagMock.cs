using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class TagMock : ITag
    {
        public List<Tag> GetByPostId(int postId)
        {
            var tags = new List<Tag>();
            var postTags = DataStorage.PostTags.FindAll(a => a.PostId == postId);
            postTags.ForEach(t => tags.AddRange(DataStorage.Tags.FindAll(a => a.TagId == t.TagId)));

            return tags;
        }

        public List<Tag> GetByName(string tagName)
        {
            var tags = DataStorage.Tags.FindAll(a => a.TagName == tagName);
            return tags;
        }

        public bool Add(Tag tag)
        {
            var id = DataStorage.Tags.Select(a => a.TagId).Max();
            tag.TagId = id + 1;
            DataStorage.Tags.Add(tag);

            return true;
        }
    }

    public class PostTag
    {
        public int PostId { get; set; }
        public int TagId { get; set; }
    }
}
