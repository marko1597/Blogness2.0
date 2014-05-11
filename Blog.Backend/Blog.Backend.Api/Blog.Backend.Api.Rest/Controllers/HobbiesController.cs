using System;
using System.Collections.Generic;
using System.Web.Http;
using Blog.Backend.Services.Implementation;
using Blog.Backend.Common.Contracts;

namespace Blog.Backend.Api.Rest.Controllers
{
    public class HobbiesController : ApiController
    {
        private readonly IHobby _service;

        public HobbiesController(IHobby hobby)
        {
            _service = hobby;
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
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);;
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
