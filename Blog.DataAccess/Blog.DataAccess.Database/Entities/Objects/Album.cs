using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Album
    {
        [Key]
        public int AlbumId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string AlbumName { get; set; }
        public bool IsUserDefault { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<Media> Media { get; set; }
    }
}
