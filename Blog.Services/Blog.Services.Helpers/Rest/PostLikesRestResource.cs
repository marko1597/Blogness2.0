using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class PostLikesRestResource : IPostLikesRestResource
    {
        public List<PostLike> Get(int postId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(PostLike postLike, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
