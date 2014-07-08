using System.ServiceModel;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IBaseService
    {
        [OperationContract]
        bool GetHeartBeat();
    }
}
