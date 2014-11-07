using System;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class UserChatMessage
    {
        public User User { get; set; }
        public ChatMessage LastChatMessage { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
