using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IAlbumService : IBaseService
    {
        [OperationContract]
        Album Get(int id);

        [OperationContract]
        List<Album> GetByUser(int userId);

        [OperationContract]
        Album GetUserDefaultGroup(int userId);

        [OperationContract]
        Album Add(Album album);

        [OperationContract]
        Album Update(Album album);

        [OperationContract]
        bool Delete(int albumId);
    }
}
