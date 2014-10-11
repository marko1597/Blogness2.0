using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class ViewCount
    {
        [Key]
        public int Id { get; set; }
        public virtual Post Post { get; set; }
        public int PostId { get; set; }
        public int? UserId { get; set; }
    }
}
