using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Interfaces
{
    public interface IAddress
    {
        Address GetByUser(int userId);
        Address Add(Address address);
        Address Update(Address address);
        bool Delete(int addressId);
    }
}
