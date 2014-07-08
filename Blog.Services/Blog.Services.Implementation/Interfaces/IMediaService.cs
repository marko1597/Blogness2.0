using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IMediaService : IBaseService
    {
        [OperationContract]
        List<Media> GetByUser(int userId);

        [OperationContract]
        List<Media> GetByGroup(int albumId);

        [OperationContract]
        Media GetByName(string customName);

        [OperationContract]
        Media Get(int mediaId);

        [OperationContract]
        Media Add(Media media, int userId);

        [OperationContract]
        Media AddAsContent(User user, string albumName, string filename, string path, string contentType);

        [OperationContract]
        bool Delete(int mediaId);
    }
}
