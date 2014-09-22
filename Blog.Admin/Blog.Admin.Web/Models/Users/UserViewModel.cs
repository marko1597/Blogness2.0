using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Contracts;
using Blog.Common.Identity.Models;

namespace Blog.Admin.Web.Models.Users
{
    public class UserViewModel : User
    {
        [DisplayName("Role")]
        public string Role { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a role for this user.")]
        public string SelectedRole { get; set; }

        [Display(Name = "Roles")]
        public List<UserRoleAvailable> RolesAvailable { get; set; }
    }

    public class CreateUserViewModel : BlogRegisterModel
    {
        public bool IdentityOnly { get; set; }

        public int Id { get; set; }

        [Display(Name = "Identity")]
        public string IdentityId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Please select a role for this user.")]
        public string SelectedRole { get; set; }

        [Display(Name = "Roles")]
        public List<UserRoleAvailable> RolesAvailable { get; set; }
    }

    public class UserRoleAvailable
    {
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public bool IsSelected { get; set; }
    }
}