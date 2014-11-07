using System;
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

            var receiverUser = Context.Users.FirstOrDefault(a => a.UserId == userId);
            var userIds = Find(a => a.FromUserId == userId)
                .Select(a => a.ToUserId)
                .Union(Find(a => a.ToUserId == userId).Select(a => a.FromUserId))
                .Distinct();

            var query = Context.Users
                .Where(a => userIds.Contains(a.UserId))
                .Join(Context.Media, u => u.PictureId, m => m.MediaId, (u, m) => new
                {
                    u.UserId, u.UserName, Picture = m, u.FirstName, u.LastName
                }).ToList();

            var users = new List<User>();
            query.ForEach(a => users.Add(new User
            {
                UserId = a.UserId,
                UserName = a.UserName,
                Picture = a.Picture,
                FirstName = a.FirstName,
                LastName = a.LastName
            }));


            foreach (var user in users)
            {
                var tUser = user;
                var messagesUnion = Find(a => a.FromUserId == tUser.UserId && a.ToUserId == userId, false)
                    .Union(Find(a => a.FromUserId == userId && a.ToUserId == tUser.UserId, false))
                    .ToList();

                var lastMessage = messagesUnion
                    .OrderByDescending(a => a.CreatedDate)
                    .Take(1)
                    .FirstOrDefault();
                
                if (lastMessage != null)
                {
                    if (lastMessage.FromUserId == userId)
                    {
                        lastMessage.FromUser = receiverUser;
                        lastMessage.ToUser = tUser;
                    }
                    else
                    {
                        lastMessage.FromUser = tUser;
                        lastMessage.ToUser = receiverUser;
                    }
                }

                var userChatMessage = new UserChatMessage
                {
                    User = user, 
                    LastChatMessage = lastMessage,
                    Timestamp = lastMessage != null ? lastMessage.CreatedDate : DateTime.Now
                };
                userChatMessages.Add(userChatMessage);
            }

            return userChatMessages.OrderByDescending(a => a.Timestamp).ToList();
        }

        public List<ChatMessage> GetChatMessages(int fromUserId, int toUserId)
        {
            var toRecipientMessages = Find(a => a.FromUserId == fromUserId && a.ToUserId == toUserId, true);
            var fromRecipientMessages = Find(a => a.FromUserId == toUserId && a.ToUserId == fromUserId, true);
            var chatMessages = toRecipientMessages
                .Union(fromRecipientMessages)
                .OrderBy(a => a.CreatedDate)
                .ToList();

            return chatMessages;
        }
    }
}
