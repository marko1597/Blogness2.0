using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IMediaResource : IMediaService
    {
    }

    public interface IMediaRestResource
    {
        List<Media> GetByUser(int userId);
        List<Media> GetByGroup(int albumId);
        Media GetByName(string customName);
        Media Get(int mediaId);
        Media Add(Media media, int userId, string authenticationToken);
        Media AddAsContent(User user, string albumName, string filename, string path, string contentType, string authenticationToken);
        bool Delete(int mediaId, string authenticationToken);
    }
}
