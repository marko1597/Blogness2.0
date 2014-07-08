using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IUsersService : IBaseService
    {
        [OperationContract]
        User GetByCredentials(string username, string password);

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
