using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Admin.Web.Models
{
    public class EducationViewModel
    {
        public List<Education> Education { get; set; }

        public List<EducationType> EducationTypes { get; set; } 
    }
}