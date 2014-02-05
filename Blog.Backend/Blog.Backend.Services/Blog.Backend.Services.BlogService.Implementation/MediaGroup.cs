using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;
using Blog.Backend.Services.BlogService.Contracts.ViewModels;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class MediaGroup : IMediaGroup
    {
        public List<UserMediaGroup> GetByUser(int userId)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().GetByUser(userId);
        }

        public Contracts.BlogObjects.MediaGroup Add(Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().Add(mediaGroup);
        }

        public Contracts.BlogObjects.MediaGroup Update(Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            return MediaGroupFactory.GetInstance().CreateMediaGroup().Update(mediaGroup);
        }

        public void Delete(Contracts.BlogObjects.MediaGroup mediaGroup)
        {
            MediaGroupFactory.GetInstance().CreateMediaGroup().Delete(mediaGroup);
        }
    }
}
