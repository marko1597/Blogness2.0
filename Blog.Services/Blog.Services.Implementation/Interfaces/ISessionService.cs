using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface ISessionService : IBaseService
    {
        [OperationContract]
        List<Session> GetAll();

        [OperationContract]
        Session GetByUser(string username);

        [OperationContract]
        Session GetByIp(string ipAddress);

        [OperationContract]
        LoggedUser Login(string userName, string passWord, string ipAddress);

        [OperationContract]
        Error Logout(string userName);
    }
}
