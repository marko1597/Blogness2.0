using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Identity.Models;

namespace Blog.Admin.Web.Models.Users
{
    public class CreateUserViewModel : BlogRegisterModel
    {
        public int Id { get; set; }

        [Display(Name = "Identity")]
        public string IdentityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a role for this user.")]
        public string SelectedRole { get; set; }

        [Display(Name = "Roles")]
        public List<CreateUserRolesAvailable> RolesAvailable { get; set; }
    }

    public class CreateUserRolesAvailable
    {
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}