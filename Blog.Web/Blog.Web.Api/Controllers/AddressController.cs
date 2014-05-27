﻿using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class AddressController : ApiController
    {
        private readonly IAddress _service;
        private readonly IErrorSignaler _errorSignaler;

        public AddressController(IAddress address, IErrorSignaler errorSignaler)
        {
            _service = address;
            _errorSignaler = errorSignaler;
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
                _errorSignaler.SignalFromCurrentContext(ex);
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