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
        ChatMessage AddChatMessage(ChatMessage chatMessage);
    }
}
