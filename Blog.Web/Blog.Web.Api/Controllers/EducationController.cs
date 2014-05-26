using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Web.Attributes;
using System;
using System.Web.Http;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Web.Api.Controllers
{
    [AllowCrossSiteApi]
    public class EducationController : ApiController
    {
        private readonly IEducation _service;
        private readonly IErrorSignaler _errorSignaler;

        public EducationController(IEducation education, IErrorSignaler errorSignaler)
        {
            _service = education;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/users/{userId:int}/education")]
        public List<Education> GetByUser(int userId)
        {
            var education = new List<Education>();
            try
            {
                education = _service.GetByUser(userId) ?? new List<Education>();
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
            return education;
        }

        [HttpPost]
        [Route("api/education")]
        public bool Post([FromBody]Education education)
        {
            try
            {
                _service.Add(education);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpPut]
        [Route("api/education")]
        public bool Put([FromBody]Education education)
        {
            try
            {
                _service.Add(education);
                return true;
            }
            catch
            {
                return false;
            }
        }

        [HttpDelete]
        [Route("api/education")]
        public bool Delete([FromBody]int educationId)
        {
            try
            {
                _service.Delete(educationId);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
