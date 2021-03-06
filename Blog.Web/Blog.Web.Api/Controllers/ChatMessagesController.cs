﻿using System;
using System.Web.Http;
using Blog.Common.Contracts;
using Blog.Common.Utils.Helpers.Elmah;
using Blog.Services.Helpers.Interfaces;

namespace Blog.Web.Api.Controllers
{
    public class ChatMessagesController : ApiController
    {
        private readonly IChatMessagesResource _chatMessagesResource;
        private readonly IErrorSignaler _errorSignaler;

        public ChatMessagesController(IChatMessagesResource chatMessagesResource, IErrorSignaler errorSignaler)
        {
            _chatMessagesResource = chatMessagesResource;
            _errorSignaler = errorSignaler;
        }

        [HttpGet, Authorize]
        [Route("api/user/{userId:int}/chats")]
        public IHttpActionResult GetListById(int userId)
        {
            try
            {
                var result = _chatMessagesResource.GetChatMessagesListByUserId(userId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        [Route("api/user/{username}/chats")]
        public IHttpActionResult GetListByName(string username)
        {
            try
            {
                var result = _chatMessagesResource.GetChatMessagesListByUsername(username);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        [Route("api/chat/{fromUserId:int}/{toUserId:int}")]
        public IHttpActionResult GetChatByIds(int fromUserId, int toUserId)
        {
            try
            {
                var result = _chatMessagesResource.GetChatMessagesByUserIds(fromUserId, toUserId);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        [Route("api/chat/{fromUsername}/{toUsername}")]
        public IHttpActionResult GetChatByNames(string fromUsername, string toUsername)
        {
            try
            {
                var result = _chatMessagesResource.GetChatMessagesByUsernames(fromUsername, toUsername);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        [Route("api/chat/{fromUserId:int}/{toUserId:int}/more/{skip:int}")]
        public IHttpActionResult GetMoreChatByIds(int fromUserId, int toUserId, int skip)
        {
            try
            {
                var result = _chatMessagesResource.GetMoreChatMessagesByUserIds(fromUserId, toUserId, skip);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpGet, Authorize]
        [Route("api/chat/{fromUsername}/{toUsername}/more/{skip:int}")]
        public IHttpActionResult GetMoreChatByNames(string fromUsername, string toUsername, int skip)
        {
            try
            {
                var result = _chatMessagesResource.GetMoreChatMessagesByUsernames(fromUsername, toUsername, skip);
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }

        [HttpPost, Authorize]
        [Route("api/chat/")]
        public IHttpActionResult Post([FromBody] ChatMessage chatMessage)
        {
            try
            {
                var result = _chatMessagesResource.AddChatMessage(chatMessage);

                if (result == null) return BadRequest();
                return Ok(result);
            }
            catch (Exception ex)
            {
                _errorSignaler.SignalFromCurrentContext(ex);
                return BadRequest();
            }
        }
    }
}
