using Blog.Common.Contracts;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf.Interfaces
{
    public interface IAddressResource : IBaseResource
    {
        Address GetByUser(int userId);

        Address Add(Address address);

        Address Update(Address address);

        bool Delete(int addressId);
    }
}
