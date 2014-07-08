using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;
using Blog.Common.Contracts;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
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

        [HttpPost]
        [Route("api/hobbies")]
        public Hobby Post([FromBody]Hobby hobby)
        {
            try
            {
                return _service.Add(hobby);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Hobby().GenerateError<Hobby>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPut]
        [Route("api/hobbies")]
        public Hobby Put([FromBody]Hobby hobby)
        {
            try
            {
                return _service.Update(hobby);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Hobby().GenerateError<Hobby>((int)Constants.Error.InternalError,
                    "Server technical error");
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
