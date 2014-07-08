using Blog.Common.Contracts;

namespace Blog.Logic.Core.Interfaces
{
    public interface IAddressLogic
    {
        Address GetByUser(int userId);
        Address Add(Address address);
        Address Update(Address address);
        bool Delete(int addressId);
    }
}
