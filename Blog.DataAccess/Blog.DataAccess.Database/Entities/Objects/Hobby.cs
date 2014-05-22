using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Hobby
    {
        [Key]
        public int HobbyId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string HobbyName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
