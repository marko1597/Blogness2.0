﻿using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Utils;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class HobbiesController : ApiController
    {
        private readonly IHobbyResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public HobbiesController(IHobbyResource hobby, IErrorSignaler errorSignaler)
        {
            _service = hobby;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/hobbies")]
        public List<Hobby> GetByUser(int userId)
        {
            var hobbies = new List<Hobby>();
            try
            {
                hobbies = _service.GetByUser(userId) ?? new List<Hobby>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return hobbies;
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/hobbies")]
        public IHttpActionResult Post([FromBody]Hobby hobby)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Add(hobby);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Hobby().GenerateError<Hobby>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/hobbies")]
        public IHttpActionResult Put([FromBody]Hobby hobby)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Update(hobby);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Hobby().GenerateError<Hobby>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/hobbies/{hobbyId}")]
        public bool Delete(int hobbyId)
        {
            try
            {
                _service.Delete(hobbyId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
