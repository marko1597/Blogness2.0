using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class PostContentsService : IPostContents
    {
        public List<PostContent> GetByPostId(int postId)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(postId);
        }

        public PostContent Get(int postContentId)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().Get(postContentId);
        }

        public PostContent Add(PostContent postImage)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().Add(postImage);
        }

        public bool Delete(int postContentId)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().Delete(postContentId);
        }
    }
}
