using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IViewCountLogic
    {
        List<ViewCount> Get(int postId);
        ViewCount Add(ViewCount viewCount);
    }
}
