using Blog.Common.Contracts;
using Blog.Common.Utils;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using System;
using System.Web.Http;
using Blog.Services.Helpers.Interfaces;

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
        public IHttpActionResult GetByUser(int userId)
        {
            try
            {
                var education = _service.GetByUser(userId);
                return Ok(education);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/education")]
        public IHttpActionResult Post([FromBody]Education education)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Add(education);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Education().GenerateError<Education>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/education")]
        public IHttpActionResult Put([FromBody]Education education)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var result = _service.Update(education);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Education().GenerateError<Education>((int)Constants.Error.InternalError,
                    "Server technical error");

                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/education/{educationId}")]
        public IHttpActionResult Delete(int educationId)
        {
            try
            {
                _service.Delete(educationId);
                return Ok(true);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
