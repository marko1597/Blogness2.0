using System;

namespace Blog.Frontend.Web.Models
{
    public class PostDetails
    {
        public int PostId { get; set; }
        public DateTime CreatedDate { get; set; }
        public Likes Likes { get; set; }
    }
}