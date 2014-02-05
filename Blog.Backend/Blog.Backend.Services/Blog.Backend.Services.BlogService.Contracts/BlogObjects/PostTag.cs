using System;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    public class PostTag
    {
        public int PostTagId { get; set; }
        public int? PostId { get; set; }
        public int? TagId { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public virtual Post Post { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
