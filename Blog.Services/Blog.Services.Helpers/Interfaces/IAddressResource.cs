using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IAddressResource : IAddressService
    {
    }

    public interface IAddressRestResource
    {
        Address GetByUser(int userId);
        Address Add(Address address, string authenticationToken);
        Address Update(Address address, string authenticationToken);
        bool Delete(int addressId, string authenticationToken);
    }
}
