using System.ComponentModel.DataAnnotations;

namespace Blog.Web.Api.Models.Account.Binding
{
    public class RegisterExternalBindingModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
    }
}