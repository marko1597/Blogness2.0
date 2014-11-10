using System.Collections.Generic;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Logic.Core.Interfaces
{
    public interface IChatMessagesLogic
    {
        ChatMessagesList GetChatMessagesListByUser(int userId);
        ChatMessagesList GetChatMessagesListByUser(string username);
        List<ChatMessage> GetChatMessagesByUser(int fromUserId, int toUserId);
        List<ChatMessage> GetChatMessagesByUser(string fromUsername, string toUsername);
        List<ChatMessage> GetMoreChatMessagesByUser(int fromUserId, int toUserId, int skip = 25);
        List<ChatMessage> GetMoreChatMessagesByUser(string fromUsername, string toUsername, int skip = 25);
        ChatMessage Add(ChatMessage chatMessage);
    }
}
