using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IPostContents
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        bool Add(PostContent postContent);
        bool Delete(int postContentId);
    }
}
