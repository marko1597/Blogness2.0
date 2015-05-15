using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Common.Web.Attributes;
using Blog.Services.Helpers.Interfaces;
using Microsoft.AspNet.Identity;
using WebApi.OutputCache.V2;
using System.Collections.Generic;

namespace Blog.Web.Api.Controllers
{
    public class CommunityController : ApiController
    {
        private readonly ICommunityResource _communityResource;
        private readonly IUsersResource _usersResource;
        private readonly IPostsResource _postsResource;
        private readonly IErrorSignaler _errorSignaler;

        public CommunityController(ICommunityResource communityResource, IUsersResource usersResource, 
            IPostsResource postsResource, IErrorSignaler errorSignaler)
        {
            _communityResource = communityResource;
            _postsResource = postsResource;
            _usersResource = usersResource;
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
        [Route("api/community/{id}/members")]
        public IHttpActionResult GetMembers(int id)
        {
            try
            {
                var members = _usersResource.GetUsersByCommunity(id, 10, 0);
                return Ok(members);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/community/{id}/members/more/{skip:int?}")]
        public IHttpActionResult GetMoreMembers(int id, int skip)
        {
            try
            {
                var members = _usersResource.GetUsersByCommunity(id, 5, skip);
                return Ok(members);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/community/{id}/posts")]
        public IHttpActionResult GetPosts(int id)
        {
            try
            {
                var posts = _postsResource.GetPostsByCommunity(id, 10, 0);
                return Ok(posts);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet]
        [CacheOutput(ClientTimeSpan = 5, ServerTimeSpan = 5)]
        [Route("api/community/{id}/posts/more/{skip:int?}")]
        public IHttpActionResult GetMorePosts(int id, int skip)
        {
            try
            {
                var posts = _postsResource.GetPostsByCommunity(id, 5, skip);
                return Ok(posts);
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

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/community/{communityId}/join")]
        public IHttpActionResult Join([FromBody]User user, int communityId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var community = _communityResource.Get(communityId);
                if (community == null) throw new Exception("Cannot update community at the moment.");

                community.Members.Add(user);

                return Ok(_communityResource.Update(community));
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

        [HttpPost, PreventCrossUserManipulation, Authorize]
        [Route("api/community/{communityId}/leave")]
        public IHttpActionResult Leave([FromBody]User user, int communityId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var community = _communityResource.Get(communityId);
                if (community == null) throw new Exception("Cannot update community at the moment.");

                var tempMembers = new List<User>();

                foreach (var member in community.Members)
                {
                    if (user.Id != member.Id)
                    {
                        tempMembers.Add(member);
                    }
                }

                community.Members = tempMembers;

                return Ok(_communityResource.Update(community));
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
    }
}
