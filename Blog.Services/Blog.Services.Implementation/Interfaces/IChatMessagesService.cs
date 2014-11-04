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
        ChatMessage AddChatMessage(ChatMessage chatMessage);
    }
}
