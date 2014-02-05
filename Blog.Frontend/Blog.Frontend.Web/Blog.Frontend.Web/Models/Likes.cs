namespace Blog.Frontend.Web.Models
{
    public class Likes
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public int Count { get; set; }
    }
}