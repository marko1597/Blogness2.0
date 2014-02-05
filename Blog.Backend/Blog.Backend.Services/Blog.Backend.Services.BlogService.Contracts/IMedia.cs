using System.Collections.Generic;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts
{
    public interface IMedia
    {
        List<Media> GetByUser(int userId);
        Media Get(int mediaId);
        Media Add(Media media);
        void Delete(int mediaId);
    }
}
