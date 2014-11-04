using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class ChatMessage
    {
        [Key]
        public int ChatMessageId { get; set; }
        public int FromUserId { get; set; }
        public virtual User FromUser { get; set; }
        public int ToUserId { get; set; }
        public virtual User ToUser { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
