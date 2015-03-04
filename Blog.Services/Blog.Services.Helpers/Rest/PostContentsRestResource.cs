using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class PostContentsRestResource : IPostContentsRestResource
    {
        public List<PostContent> GetByPostId(int postId)
        {
            throw new System.NotImplementedException();
        }

        public PostContent Get(int postContentId)
        {
            throw new System.NotImplementedException();
        }

        public PostContent Add(PostContent postContent, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public bool Delete(int postContentId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
