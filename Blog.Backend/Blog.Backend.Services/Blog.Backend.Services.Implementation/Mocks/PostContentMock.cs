using System;
using System.Collections.Generic;
using System.Linq;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation.Mocks
{
    public class PostContentMock : IPostContents
    {
        public List<PostContent> GetByPostId(int postId)
        {
            var postContent = DataStorage.PostContents.FindAll(a => a.PostId == postId);
            return postContent;
        }

        public PostContent Get(int postContentId)
        {
            var postContent = DataStorage.PostContents.FirstOrDefault(a => a.PostContentId == postContentId);
            return postContent;
        }

        public bool Add(PostContent postContent)
        {
            var id = DataStorage.PostContents.Select(a => a.PostContentId).Max();
            postContent.PostContentId = id + 1;
            DataStorage.PostContents.Add(postContent);

            return true;
        }

        public bool Delete(int postContentId)
        {
            var tPostContent = DataStorage.PostContents.FirstOrDefault(a => a.PostContentId == postContentId);
            DataStorage.PostContents.Remove(tPostContent);

            return true;
        }
    }
}
