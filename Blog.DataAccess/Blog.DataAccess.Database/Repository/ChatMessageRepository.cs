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

            var receiverUser = Context.Users.Where(a => a.UserId == userId).FirstOrDefault();
            var userIds = Find(a => a.FromUserId == userId)
                .Select(a => a.ToUserId)
                .Union(Find(a => a.ToUserId == userId).Select(a => a.FromUserId))
                .Distinct();

            var query = Context.Users
                .Where(a => userIds.Contains(a.UserId))
                .Join(Context.Media, u => u.PictureId, m => m.MediaId, (u, m) => new
                {
                    UserId = u.UserId,
                    UserName = u.UserName,
                    Picture = m,
                    FirstName = u.FirstName,
                    LastName = u.LastName
                }).ToList();

            var users = new List<User>();
            query.ForEach(a =>
            {
                users.Add(new User
                {
                    UserId = a.UserId,
                    UserName = a.UserName,
                    Picture = a.Picture,
                    FirstName = a.FirstName,
                    LastName = a.LastName
                });
            });


            foreach (var user in users)
            {
                var tUser = user;
                var lastMessage = Find(a => a.ToUserId == tUser.UserId, false)
                    .OrderBy(a => a.CreatedDate)
                    .Take(1)
                    .ToList();

                if (lastMessage.Count == 0)
                {
                    lastMessage = Find(a => a.FromUserId == userId && a.ToUserId == tUser.UserId, false)
                        .OrderBy(a => a.CreatedDate)
                        .Take(1)
                        .ToList();

                    lastMessage.FirstOrDefault().FromUser = receiverUser;
                    lastMessage.FirstOrDefault().ToUser = tUser;
                }
                else
                {
                    lastMessage.FirstOrDefault().FromUser = tUser;
                    lastMessage.FirstOrDefault().ToUser = receiverUser;
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
                .OrderBy(a => a.CreatedDate)
                .ToList();

            return chatMessages;
        }
    }
}
