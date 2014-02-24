using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public virtual ICollection<Post> Posts { get; set; }
    }
}
