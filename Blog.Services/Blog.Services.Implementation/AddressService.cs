using Blog.Common.Contracts;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    public class AddressService : BaseService, IAddressService
    {
        private readonly IAddressLogic _addressLogic;

        public AddressService(IAddressLogic addressLogic)
        {
            _addressLogic = addressLogic;
        }

        public Address GetByUser(int userId)
        {
            return _addressLogic.GetByUser(userId);
        }

        public Address Add(Address address)
        {
            return _addressLogic.Add(address);
        }

        public Address Update(Address address)
        {
            return _addressLogic.Update(address);
        }

        public bool Delete(int addressId)
        {
            return _addressLogic.Delete(addressId);
        }
    }
}
