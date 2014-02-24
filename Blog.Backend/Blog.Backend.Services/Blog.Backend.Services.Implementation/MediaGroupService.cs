using System.Collections.Generic;
using Blog.Backend.Logic.Factory;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public class MediaGroupService : IMediaGroup
    {
        public List<MediaGroup> GetByUser(int userId)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().GetByUser(userId);
        }

        public MediaGroup GetUserDefaultGroup(int userId)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().GetUserDefaultGroup(userId);
        }

        public bool Add(MediaGroup mediaGroup)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().Add(mediaGroup);
        }

        public bool Update(MediaGroup mediaGroup)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().Update(mediaGroup);
        }

        public bool Delete(MediaGroup mediaGroup)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().Delete(mediaGroup);
        }
    }
}
