using System.ComponentModel.DataAnnotations;

namespace Blog.Admin.Web.Models
{
    public class UserRoleViewModel
    {
        [Display(Name = "Role ID")]
        public string Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Role Name")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [Display(Name = "Description")]
        public string Description { get; set; }
    }
}