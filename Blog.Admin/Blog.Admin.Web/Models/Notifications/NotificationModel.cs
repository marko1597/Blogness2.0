using System.ComponentModel.DataAnnotations;

namespace Blog.Admin.Web.Models.Notifications
{
    public class NotificationModel
    {
        public string User { get; set; }

        public int? ChannelId { get; set; }

        [Required]
        public string Type { get; set; }

        public string Message { get; set; }
    }
}