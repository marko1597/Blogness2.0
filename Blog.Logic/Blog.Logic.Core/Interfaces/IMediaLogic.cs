using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IMediaLogic
    {
        List<Media> GetByUser(int userId);
        List<Media> GetByAlbum(int albumId);
        Media Get(int mediaId);
        Media GetByName(string customName);
        Media Add(Media media, int userId);
        Media Add(Common.Contracts.User user, string albumName, string filename, string path, string contentType);
        bool Delete(int mediaId);
    }
}
