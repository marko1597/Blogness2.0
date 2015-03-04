using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class ViewCountRestResource : IViewCountRestResource
    {
        public List<ViewCount> Get(int postId)
        {
            throw new System.NotImplementedException();
        }

        public void Add(ViewCount viewCount, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
