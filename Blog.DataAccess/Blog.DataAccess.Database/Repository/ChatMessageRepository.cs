using System.Collections.Generic;
using System.Linq;
using Blog.DataAccess.Database.Entities;
using Blog.DataAccess.Database.Entities.Objects;
using Blog.DataAccess.Database.Repository.Interfaces;

namespace Blog.DataAccess.Database.Repository
{
    public class ChatMessageRepository : GenericRepository<BlogDb, ChatMessage>, IChatMessageRepository
    {
        public List<UserChatMessage> GetUserChatMessages(int userId)
        {
            var userChatMessages = new List<UserChatMessage>();
            var users = Find(a => a.FromUserId == userId).Select(a => a.ToUser).Distinct();

            foreach (var user in users)
            {
                var tUser = user;
                var lastMessage = Find(a => a.ToUserId == tUser.UserId)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(1)
                    .ToList();

                if (lastMessage.Count == 0)
                {
                    lastMessage = Find(a => a.FromUserId == userId && a.ToUserId == tUser.UserId)
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(1)
                    .ToList();
                }

                var userChatMessage = new UserChatMessage { User = user, ChatMessages = lastMessage };
                userChatMessages.Add(userChatMessage);
            }

            return userChatMessages;
        }
    }
}
