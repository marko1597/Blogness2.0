using System.Collections.Generic;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        List<UserChatMessage> GetUserChatMessages(int userId);
        List<ChatMessage> GetChatMessages(int fromUserId, int toUserId);
    }
}
