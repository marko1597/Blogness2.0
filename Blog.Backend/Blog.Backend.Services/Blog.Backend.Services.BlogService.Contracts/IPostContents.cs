using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IPostContents
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        void Add(PostContent postContent);
        void Delete(int postContentId);
    }
}
