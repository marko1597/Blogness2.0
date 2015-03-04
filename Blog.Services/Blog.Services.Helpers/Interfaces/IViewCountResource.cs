using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IViewCountResource : IViewCountService
    {
    }

    public interface IViewCountRestResource
    {
        List<ViewCount> Get(int postId);
        void Add(ViewCount viewCount, string authenticationToken);
    }
}
