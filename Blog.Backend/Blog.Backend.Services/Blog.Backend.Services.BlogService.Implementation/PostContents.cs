using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class PostContents : IPostContents
    {
        public List<PostContent> GetByPostId(int postId)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().GetByPostId(postId);
        }

        public PostContent Get(int postContentId)
        {
            return PostContentsFactory.GetInstance().CreatePostContents().Get(postContentId);
        }

        public void Add(PostContent postImage)
        {
            PostContentsFactory.GetInstance().CreatePostContents().Add(postImage);
        }

        public void Delete(int postContentId)
        {
            PostContentsFactory.GetInstance().CreatePostContents().Delete(postContentId);
        }
    }
}
