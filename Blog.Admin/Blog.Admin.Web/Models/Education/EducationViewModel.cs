using System.Collections.Generic;
using Blog.Common.Contracts;

namespace Blog.Admin.Web.Models.Education
{
    public class EducationViewModel
    {
        public List<Common.Contracts.Education> Education { get; set; }

        public List<EducationType> EducationTypes { get; set; } 
    }
}