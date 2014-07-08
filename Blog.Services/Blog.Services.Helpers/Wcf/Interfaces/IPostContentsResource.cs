using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IPostContentsResource : IBaseResource
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        PostContent Add(PostContent postContent);
        bool Delete(int postContentId);
    }
}
