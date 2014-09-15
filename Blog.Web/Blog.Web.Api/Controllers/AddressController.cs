using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
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

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/address")]
        public IHttpActionResult Post([FromBody]Address address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Add(address);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Address().GenerateError<Address>((int) Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/address")]
        public IHttpActionResult Put([FromBody]Address address)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Update(address);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Address().GenerateError<Address>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/address/{addressId}")]
        public bool Delete(int addressId)
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
