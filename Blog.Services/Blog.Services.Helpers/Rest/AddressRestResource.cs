using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class AddressRestResource : IAddressResource
    {
        public Address GetByUser(int userId)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Address>(svc.Get(Constants.BlogRestUrl, string.Format("users/{0}/address", userId)));
                return result;
            }
        }

        public Address Add(Address address)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Address>(svc.Post(Constants.BlogRestUrl, "address", address));
                return result;
            }
        }

        public Address Update(Address address)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<Address>(svc.Put(Constants.BlogRestUrl, "address", address));
                return result;
            }
        }

        public bool Delete(int addressId)
        {
            using (var svc = new HttpClientHelper())
            {
                var result = JsonHelper.DeserializeJson<bool>(svc.Delete(Constants.BlogRestUrl, string.Format("address/{0}", addressId)));
                return result;
            }
        }

        public bool GetHeartBeat()
        {
            return false;
        }
    }
}
