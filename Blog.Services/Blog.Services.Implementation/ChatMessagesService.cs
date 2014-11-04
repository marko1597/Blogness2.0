using System.Collections.Generic;
using System.ServiceModel.Activation;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
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

        public ChatMessagesService(IChatMessagesLogic chatMessagesLogic)
        {
            _chatMessagesLogic = chatMessagesLogic;
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

        public ChatMessage AddChatMessage(ChatMessage chatMessage)
        {
            return _chatMessagesLogic.AddChatMessage(chatMessage);
        }
    }
}
