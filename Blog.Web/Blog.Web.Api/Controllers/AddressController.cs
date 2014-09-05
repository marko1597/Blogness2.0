using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class AddressController : ApiController
    {
        private readonly IAddressResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public AddressController(IAddressResource address, IErrorSignaler errorSignaler)
        {
            _service = address;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/address")]
        public Address GetByUser(int userId)
        {
            try
            {
                return _service.GetByUser(userId) ?? new Address();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Address().GenerateError<Address>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPost]
        [Route("api/address")]
        public Address Post([FromBody]Address address)
        {
            try
            {
                return _service.Add(address);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Address().GenerateError<Address>((int) Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPut]
        [Route("api/address")]
        public Address Put([FromBody]Address address)
        {
            try
            {
                return _service.Update(address);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Address().GenerateError<Address>((int)Constants.Error.InternalError,
                    "Server technical error");
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
