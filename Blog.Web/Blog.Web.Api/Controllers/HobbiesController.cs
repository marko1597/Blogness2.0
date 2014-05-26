using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Web.Attributes;
using Blog.Common.Contracts;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class HobbiesController : ApiController
    {
        private readonly IHobby _service;
        private readonly IErrorSignaler _errorSignaler;

        public HobbiesController(IHobby hobby, IErrorSignaler errorSignaler)
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

        [HttpPost]
        [Route("api/hobbies")]
        public bool Post([FromBody]Hobby hobby)
        {
            try
            {
                _service.Add(hobby);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/hobbies")]
        public bool Put([FromBody]Hobby hobby)
        {
            try
            {
                _service.Add(hobby);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/hobbies")]
        public bool Delete([FromBody]int hobbyId)
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
