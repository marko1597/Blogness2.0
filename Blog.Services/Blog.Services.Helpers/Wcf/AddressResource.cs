using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Wcf.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Wcf
{
    [ExcludeFromCodeCoverage]
    public class AddressResource :  IAddressResource
    {
        public Address GetByUser(int userId)
        {
            using (var svc = new ServiceProxyHelper<IAddressService>("AddressService"))
            {
                return svc.Proxy.GetByUser(userId);
            }
        }

        public Address Add(Address address)
        {
            using (var svc = new ServiceProxyHelper<IAddressService>("AddressService"))
            {
                return svc.Proxy.Add(address);
            }
        }

        public Address Update(Address address)
        {
            using (var svc = new ServiceProxyHelper<IAddressService>("AddressService"))
            {
                return svc.Proxy.Update(address);
            }
        }

        public bool Delete(int addressId)
        {
            using (var svc = new ServiceProxyHelper<IAddressService>("AddressService"))
            {
                return svc.Proxy.Delete(addressId);
            }
        }

        public bool GetHeartBeat()
        {
            using (var svc = new ServiceProxyHelper<IAddressService>("AddressService"))
            {
                return svc.Proxy.GetHeartBeat();
            }
        }
    }
}
