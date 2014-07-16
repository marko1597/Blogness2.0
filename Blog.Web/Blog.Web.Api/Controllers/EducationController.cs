using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Web.Attributes;
using System;
using System.Web.Http;
using Blog.Common.Web.Extensions.Elmah;
using Blog.Services.Helpers.Wcf.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class EducationController : ApiController
    {
        private readonly IEducationResource _service;
        private readonly IErrorSignaler _errorSignaler;

        public EducationController(IEducationResource education, IErrorSignaler errorSignaler)
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
        public Education Post([FromBody]Education education)
        {
            try
            {
                return _service.Add(education);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Education().GenerateError<Education>((int)Constants.Error.InternalError,
                    "Server technical error");
            }
        }

        [HttpPut]
        [Route("api/education")]
        public Education Put([FromBody]Education education)
        {
            try
            {
                return _service.Update(education);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return new Education().GenerateError<Education>((int)Constants.Error.InternalError,
                    "Server technical error");
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
