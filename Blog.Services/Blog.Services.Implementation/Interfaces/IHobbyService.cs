using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IHobbyService : IBaseService
    {
        [OperationContract]
        List<Hobby> GetByUser(int userId);

        [OperationContract]
        Hobby Add(Hobby hobby);

        [OperationContract]
        Hobby Update(Hobby hobby);

        [OperationContract]
        bool Delete(int hobbyId);
    }
}
