using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IUsersService : IBaseService
    {
        [OperationContract]
        List<User> GetUsers(int threshold = 10, int skip = 10);

        [OperationContract]
        List<User> GetUsersWithNoIdentityId();

        [OperationContract]
        User GetByUserName(string username);

        [OperationContract]
        User Get(int userId);

        [OperationContract]
        User Add(User user);

        [OperationContract]
        User Update(User user);
    }
}
