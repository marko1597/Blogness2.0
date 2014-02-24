using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IPostContents
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        bool Add(PostContent postContent);
        bool Delete(int postContentId);
    }
}
