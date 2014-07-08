using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IPostContentsLogic
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        PostContent Add(PostContent postContent);
        bool Delete(int postContentId);
    }
}
