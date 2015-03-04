using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Common.Utils.Helpers;
using Blog.Services.Helpers.Interfaces;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Rest
{
    [ExcludeFromCodeCoverage]
    public class ChatMessagesRestResource : IChatMessagesRestResource
    {
        public ChatMessagesList GetChatMessagesListByUserId(int userId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public ChatMessagesList GetChatMessagesListByUsername(string username, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }

        public List<ChatMessage> GetMoreChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken, int skip = 25)
        {
            throw new System.NotImplementedException();
        }

        public List<ChatMessage> GetMoreChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken, int skip = 25)
        {
            throw new System.NotImplementedException();
        }

        public ChatMessage AddChatMessage(ChatMessage chatMessage, string authenticationToken)
        {
            throw new System.NotImplementedException();
        }
    }
}
