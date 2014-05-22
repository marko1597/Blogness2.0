using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;

namespace Blog.Services.Implementation
{
    public class AddressService : IAddress
    {
        public Address GetByUser(int userId)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().GetByUser(userId);
        }

        public bool Add(Address address)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Add(address);
        }

        public bool Update(Address address)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Update(address);
        }

        public bool Delete(int addressId)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Delete(addressId);
        }
    }
}
