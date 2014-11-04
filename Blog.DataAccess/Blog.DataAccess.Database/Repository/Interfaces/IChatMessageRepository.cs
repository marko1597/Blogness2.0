using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Blog.DataAccess.Database.Entities.Objects;

namespace Blog.DataAccess.Database.Repository.Interfaces
{
    public interface IChatMessageRepository : IGenericRepository<ChatMessage>
    {
        List<UserChatMessage> GetUserChatMessages(int userId);
    }
}
