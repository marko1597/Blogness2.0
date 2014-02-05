namespace Blog.Frontend.Web.Models
{
    public class CommentAdd
    {
        public int PostId { get; set; }
        public int? ParentCommentId { get; set; }
        public string CommentLocation { get; set; }
        public string CommentMessage { get; set; }
    }
}