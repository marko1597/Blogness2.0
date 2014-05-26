﻿using System;
using System.Web.Http;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;

namespace Blog.Web.Api.Controllers
{
    public class DebugController : ApiController
    {
        private readonly IErrorSignaler _errorSignaler;

        public DebugController(IErrorSignaler errorSignaler)
        {
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/debug/sendmessage/{message}")]
        [BlogApiAuthorization]
        public string Get(string message)
        {
            try
            {
                return message;
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return string.Empty;
            }
        }
    }
}
