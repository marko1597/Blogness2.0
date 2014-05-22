using System.Linq;
using Blog.Common.Contracts;

namespace Blog.Services.Implementation.Mocks
{
    public class AddressMock : IAddress
    {
        public Address GetByUser(int userId)
        {
            var address = DataStorage.Addresses.FirstOrDefault(a => a.UserId == userId);
            return address;
        }

        public bool Add(Address address)
        {
            var id = DataStorage.Addresses.Select(a => a.AddressId).Max();
            address.AddressId = id + 1;
            DataStorage.Addresses.Add(address);

            return true;
        }

        public bool Update(Address address)
        {
            var tAddress = DataStorage.Addresses.FirstOrDefault(a => a.AddressId == address.AddressId);
            DataStorage.Addresses.Remove(tAddress);
            DataStorage.Addresses.Add(address);

            return true;
        }

        public bool Delete(int addressId)
        {
            var tAddress = DataStorage.Addresses.FirstOrDefault(a => a.AddressId == addressId);
            DataStorage.Addresses.Remove(tAddress);

            return true;
        }
    }
}
