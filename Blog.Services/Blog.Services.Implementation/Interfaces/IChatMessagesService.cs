using System.Collections.Generic;
using System.ServiceModel;
using Blog.Common.Contracts;
using Blog.Common.Contracts.ViewModels;

namespace Blog.Services.Implementation.Interfaces
{
    [ServiceContract]
    public interface IChatMessagesService : IBaseService
    {
        [OperationContract]
        ChatMessagesList GetChatMessagesListByUserId(int userId);

        [OperationContract]
        ChatMessagesList GetChatMessagesListByUsername(string username);

        [OperationContract]
        List<ChatMessage> GetChatMessagesByUserIds(int fromUserId, int toUserId);

        [OperationContract]
        List<ChatMessage> GetChatMessagesByUsernames(string fromUsername, string toUsername);

        [OperationContract]
        List<ChatMessage> GetMoreChatMessagesByUserIds(int fromUserId, int toUserId, int skip = 25);

        [OperationContract]
        List<ChatMessage> GetMoreChatMessagesByUsernames(string fromUsername, string toUsername, int skip = 25);

        [OperationContract]
        ChatMessage AddChatMessage(ChatMessage chatMessage);
    }
}
