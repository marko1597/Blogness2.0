using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Api.Models.Account.Binding
{
    public class AddExternalLoginBindingModel
    {
        [Required]
        [Display(Name = "External access token")]
        public string ExternalAccessToken { get; set; }
    }
}