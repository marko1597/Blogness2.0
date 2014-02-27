using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Services.Implementation
{
    public interface IAddress
    {
        Address GetByUser(int userId);
        bool Add(Address education);
        bool Update(Address education);
        bool Delete(int addressId);
    }
}
