using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class TagMock : ITag
    {
        public TagMock()
        {
            if (DataStorage.Tags.Count == 0)
            {
                DataStorage.Tags.Add(new Tag
                {
                    CreatedBy = 1,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = 1,
                    ModifiedDate = DateTime.UtcNow,
                    TagId = 1,
                    TagName = "lorem"
                });
                DataStorage.Tags.Add(new Tag
                {
                    CreatedBy = 2,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = 2,
                    ModifiedDate = DateTime.UtcNow,
                    TagId = 2,
                    TagName = "ipsum"
                });
                DataStorage.Tags.Add(new Tag
                {
                    CreatedBy = 3,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedBy = 3,
                    ModifiedDate = DateTime.UtcNow,
                    TagId = 3,
                    TagName = "dolor"
                });
            }

            if (DataStorage.PostTags.Count == 0)
            {
                foreach (var p in DataStorage.Posts)
                {
                    DataStorage.Tags.ForEach(t => DataStorage.PostTags.Add(new PostTag
                                                                           {
                                                                               PostId = p.PostId, 
                                                                               TagId = t.TagId
                                                                           }));
                }
            }
        }

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
