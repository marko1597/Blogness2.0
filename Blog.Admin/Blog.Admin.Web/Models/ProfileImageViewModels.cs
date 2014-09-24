using System.ComponentModel.DataAnnotations;
using System.Web;
using Blog.Common.Contracts;

namespace Blog.Admin.Web.Models
{
    public class ImageUploadViewModel
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public bool IsBackground { get; set; }

        [Required(ErrorMessage = "Please select an image from your computer.")]
        [DataType(DataType.Upload)]
        [Display(Name = "Select image")]
        public HttpPostedFileBase ImageUpload { get; set; }

        [Display(Name = "Current selected image")]
        public string MediaUrl { get; set; }
    }
}