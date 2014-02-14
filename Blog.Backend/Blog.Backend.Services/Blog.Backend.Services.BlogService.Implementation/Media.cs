using System.Collections.Generic;
using Blog.Backend.Logic.BlogService.Factory;
using Blog.Backend.Services.BlogService.Contracts;

namespace Blog.Backend.Services.BlogService.Implementation
{
    public class Media : IMedia
    {
        public List<Contracts.BlogObjects.Media> GetByUser(int userId)
        {
            return MediaFactory.GetInstance().CreateMedia().GetByUser(userId);
        }

        public Contracts.BlogObjects.Media Get(int mediaId)
        {
            return MediaFactory.GetInstance().CreateMedia().Get(mediaId);
        }

        public Contracts.BlogObjects.Media Add(Contracts.BlogObjects.Media media)
        {
            return MediaFactory.GetInstance().CreateMedia().Add(media);
        }

        public void Delete(int mediaId)
        {
            MediaFactory.GetInstance().CreateMedia().Delete(mediaId);
        }
    }
}
