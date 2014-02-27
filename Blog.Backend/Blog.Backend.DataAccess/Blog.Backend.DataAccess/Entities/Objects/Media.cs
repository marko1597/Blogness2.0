using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public virtual User User { get; set; }
        public int UserId { get; set; }
        public string MediaType { get; set; }
        public virtual MediaGroup MediaGroup { get; set; }
        public int MediaGroupId { get; set; }
        public string FileName { get; set; }
        public string CustomName { get; set; }
        public string MediaUrl { get; set; }
        public string MediaPath { get; set; }
        public byte[] MediaContent { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ThumbnailPath { get; set; }
        public byte[] ThumbnailContent { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
