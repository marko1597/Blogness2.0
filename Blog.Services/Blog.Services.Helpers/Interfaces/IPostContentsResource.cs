using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IPostContentsResource : IPostContentsService
    {
    }

    public interface IPostContentsRestResource
    {
        List<PostContent> GetByPostId(int postId);
        PostContent Get(int postContentId);
        PostContent Add(PostContent postContent, string authenticationToken);
        bool Delete(int postContentId, string authenticationToken);
    }
}
