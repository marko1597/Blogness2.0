using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;
using Blog.Services.Implementation.Interfaces;

namespace Blog.Services.Helpers.Interfaces
{
    public interface IChatMessagesResource : IChatMessagesService
    {
    }

    public interface IChatMessagesRestResource
    {
        ChatMessagesList GetChatMessagesListByUserId(int userId, string authenticationToken);
        ChatMessagesList GetChatMessagesListByUsername(string username, string authenticationToken);
        List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken);
        List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken);
        List<ChatMessage> GetMoreChatMessagesByUserIds(int fromUserId, int toUserId, string authenticationToken, int skip = 25);
        List<ChatMessage> GetMoreChatMessagesByUsernames(string fromUsername, string toUsername, string authenticationToken, int skip = 25);
        ChatMessage AddChatMessage(ChatMessage chatMessage, string authenticationToken);
    }
}
