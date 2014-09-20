using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Blog.Common.Contracts;
using Blog.Common.Identity.User;

namespace Blog.Admin.Web.Models.Identity
{
    public class MapIdentityViewModel
    {
        public User User { get; set; }

        [Required]
        [Display(Name = "Selected Identity for this user")]
        public string SelectedIdentityId { get; set; }

        [Display(Name = "Identities")]
        public List<BlogUser> BlogUsers { get; set; }
    }
}