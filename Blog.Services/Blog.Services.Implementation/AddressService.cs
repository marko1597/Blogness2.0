using Blog.Common.Contracts;
using Blog.Logic.Core.Factory;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class AddressService : IAddress
    {
        public Address GetByUser(int userId)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().GetByUser(userId);
        }

        public Address Add(Address address)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Add(address);
        }

        public Address Update(Address address)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Update(address);
        }

        public bool Delete(int addressId)
        {
            return AddressFactory.GetInstance().CreateAddressLogic().Delete(addressId);
        }
    }
}
