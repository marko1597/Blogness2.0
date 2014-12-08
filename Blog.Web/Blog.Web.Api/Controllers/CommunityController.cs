using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Wcf.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;

namespace Blog.Web.Api.Controllers
{
    public class CommunityController : ApiController
    {
        private readonly ICommunityResource _communityResource;
        private readonly IErrorSignaler _errorSignaler;

        public CommunityController(ICommunityResource communityResource, IErrorSignaler errorSignaler)
        {
            _communityResource = communityResource;
            _errorSignaler = errorSignaler;
        }

        [HttpGet]
        [Route("api/community/{id}")]
        public IHttpActionResult GetById(int id)
        {
            try
            {
                var community = _communityResource.Get(id);
                return Ok(community);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/community")]
        public IHttpActionResult GetList()
        {
            try
            {
                var communities = _communityResource.GetList();
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/community/more/{skip:int?}")]
        public IHttpActionResult GetMore(int skip = 10)
        {
            try
            {
                var communities = _communityResource.GetMore(skip);
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId:int}/communities/created")]
        public IHttpActionResult GetCreatedByUser(int userId)
        {
            try
            {
                var communities = _communityResource.GetCreatedByUser(userId);
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/communities/created/more/{skip:int?}")]
        public IHttpActionResult GetMoreCreatedByUser(int userId, int skip = 10)
        {
            try
            {
                var communities = _communityResource.GetMoreCreatedByUser(userId, skip);
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId:int}/communities/joined")]
        public IHttpActionResult GetJoinedByUser(int userId)
        {
            try
            {
                var communities = _communityResource.GetJoinedByUser(userId);
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/user/{userId}/communities/joined/more/{skip:int?}")]
        public IHttpActionResult GetMoreJoinedByUser(int userId, int skip = 10)
        {
            try
            {
                var communities = _communityResource.GetMoreJoinedByUser(userId, skip);
                return Ok(communities);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/community")]
        public IHttpActionResult Post([FromBody]Community community)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return Ok(_communityResource.Add(community));
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Community
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
                return Ok(errorResult);
            }
        }

        [HttpPut, PreventCrossUserManipulation, Authorize]
        [Route("api/community")]
        public IHttpActionResult Put([FromBody]Community community)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var tCommunity = _communityResource.Get(community.Id);
                var isAllowed = User.Identity.GetUserName() == tCommunity.Leader.UserName;

                if (isAllowed) return Ok(_communityResource.Update(community));

                var notAllowedResult = new Community
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.RequestNotAllowed,
                        Message = "Request not allowed. You cannot edit someone else's community."
                    }
                };
                return Ok(notAllowedResult);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                var errorResult = new Community
                {
                    Error = new Error
                    {
                        Id = (int)Common.Utils.Constants.Error.InternalError,
                        Message = ex.Message
                    }
                };
                return Ok(errorResult);
            }
        }

        [HttpDelete]
        [Route("api/community/{id:int}")]
        public void Delete(int id)
        {
            try
            {
                _communityResource.Delete(id);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
            }
        }
    }
}
