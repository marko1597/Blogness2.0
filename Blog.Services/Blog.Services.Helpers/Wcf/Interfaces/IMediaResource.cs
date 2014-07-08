using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IMediaResource : IBaseResource
    {
        List<Media> GetByUser(int userId);
        List<Media> GetByGroup(int albumId);
        Media GetByName(string customName);
        Media Get(int mediaId);
        Media Add(Media media, int userId);
        Media Add(User user, string albumName, string filename, string path, string contentType);
        bool Delete(int mediaId);
    }
}
