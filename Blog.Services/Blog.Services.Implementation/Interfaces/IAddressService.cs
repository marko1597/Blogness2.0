using System.ServiceModel;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IAddressService : IBaseService
    {
        [OperationContract]
        Address GetByUser(int userId);

        [OperationContract]
        Address Add(Address address);

        [OperationContract]
        Address Update(Address address);

        [OperationContract]
        bool Delete(int addressId);
    }
}
