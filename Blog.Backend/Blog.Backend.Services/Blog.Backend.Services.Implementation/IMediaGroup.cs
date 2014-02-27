using System.Collections.Generic;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IMediaGroup
    {
        List<MediaGroup> GetByUser(int userId);
        MediaGroup GetUserDefaultGroup(int userId);
        bool Add(MediaGroup mediaGroup);
        bool Update(MediaGroup mediaGroup);
        bool Delete(int mediaGroupId);
    }
}
