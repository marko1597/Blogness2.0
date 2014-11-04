using System.Collections.Generic;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class UserChatMessage
    {
        public User User { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}
