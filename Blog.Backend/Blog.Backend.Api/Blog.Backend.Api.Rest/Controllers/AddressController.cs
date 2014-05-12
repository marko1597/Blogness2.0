using System;
using System.Web.Http;
using Blog.Backend.Common.Contracts;
using Blog.Backend.Common.Web.Attributes;
using Blog.Backend.Services.Implementation;

namespace Blog.Backend.Api.Rest.Controllers
{
    [AllowCrossSiteApi]
    public class AddressController : ApiController
    {
        private readonly IAddress _service;

        public AddressController(IAddress address)
        {
            _service = address;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/address")]
        public Address GetByUser(int userId)
        {
            var address = new Address();
            try
            {
                address = _service.GetByUser(userId) ?? new Address();
            }
            catch (Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            }
            return address;
        }

        [HttpPost]
        [Route("api/address")]
        public bool Post([FromBody]Address address)
        {
            try
            {
                _service.Add(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/address")]
        public bool Put([FromBody]Address address)
        {
            try
            {
                _service.Add(address);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/address")]
        public bool Delete([FromBody]int addressId)
        {
            try
            {
                _service.Delete(addressId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
