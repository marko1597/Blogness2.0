using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Contracts;

namespace Blog.Admin.Web.Models.Users
{
    public class UserViewModel : User
    {
        [Required]
        [DisplayName("Role")]
        public string Role { get; set; }
    }
}