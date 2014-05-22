using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Media
    {
        [Key]
        public int MediaId { get; set; }
        public string MediaType { get; set; }
        public virtual Album Album { get; set; }
        public int AlbumId { get; set; }
        public string FileName { get; set; }
        public string CustomName { get; set; }
        public string MediaUrl { get; set; }
        public string MediaPath { get; set; }
        public string ThumbnailUrl { get; set; }
        public string ThumbnailPath { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
    }
}
