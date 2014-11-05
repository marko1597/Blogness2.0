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
            var userIds = Find(a => a.FromUserId == userId)
                .Select(a => a.ToUserId)
                .Distinct();
            var users = Context.Users.Where(a => userIds.Contains(a.UserId)).ToList();
            
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

        public List<ChatMessage> GetChatMessages(int fromUserId, int toUserId)
        {
            var toRecipientMessages = Find(a => a.FromUserId == fromUserId && a.ToUserId == toUserId, true);
            var fromRecipientMessages = Find(a => a.FromUserId == toUserId && a.ToUserId == fromUserId, true);
            var chatMessages = toRecipientMessages
                .Union(fromRecipientMessages)
                .OrderByDescending(a => a.CreatedDate)
                .ToList();

            return chatMessages;
        }
    }
}
