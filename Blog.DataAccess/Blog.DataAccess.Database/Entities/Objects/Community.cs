using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Community
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public int LeaderUserId { get; set; }
        public virtual User Leader { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<User> Members { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
