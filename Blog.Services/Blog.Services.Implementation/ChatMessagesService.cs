using System;
using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Contracts.ViewModels.SocketViewModels;
using Blog.Common.Utils;
using Blog.Logic.Core.Interfaces;
using Blog.Services.Implementation.Attributes;
using Blog.Services.Implementation.Handlers;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Implementation
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    [ServiceErrorBehaviour(typeof(HttpErrorHandler))]
    public class ChatMessagesService: BaseService, IChatMessagesService
    {
        private readonly IChatMessagesLogic _chatMessagesLogic;
        private readonly IRedisService _redisService;

        public ChatMessagesService(IChatMessagesLogic chatMessagesLogic, IRedisService redisService)
        {
            _chatMessagesLogic = chatMessagesLogic;
            _redisService = redisService;
        }

        public ChatMessagesList GetChatMessagesListByUserId(int userId)
        {
            return _chatMessagesLogic.GetChatMessagesListByUser(userId);
        }

        public ChatMessagesList GetChatMessagesListByUsername(string username)
        {
            return _chatMessagesLogic.GetChatMessagesListByUser(username);
        }

        public List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId)
        {
            return _chatMessagesLogic.GetChatMessagesByUser(fromUserId, toUserId);
        }

        public List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername)
        {
            return _chatMessagesLogic.GetChatMessagesByUser(fromUsername, toUsername);
        }

        public List<ChatMessage> GetMoreChatMessagesByUserIds(int fromUserId, int toUserId, int skip = 25)
        {
            return _chatMessagesLogic.GetMoreChatMessagesByUser(fromUserId, toUserId, skip);
        }

        public List<ChatMessage> GetMoreChatMessagesByUsernames(string fromUsername, string toUsername, int skip = 25)
        {
            return _chatMessagesLogic.GetMoreChatMessagesByUser(fromUsername, toUsername, skip);
        }

        public ChatMessage AddChatMessage(ChatMessage chatMessage)
        {
            var result = _chatMessagesLogic.Add(chatMessage);
            if (result != null && result.Error != null) throw new Exception(result.Error.Message);
            if (result == null) return null;

            var sendChatMessage = new SendChatMessage
            {
                ChatMessage = result,
                RecipientUserId = result.ToUser.Id,
                ClientFunction = Constants.SocketClientFunctions.SendChatMessage.ToString()
            };

            _redisService.Publish(sendChatMessage);
            return result;
        }
    }
}
