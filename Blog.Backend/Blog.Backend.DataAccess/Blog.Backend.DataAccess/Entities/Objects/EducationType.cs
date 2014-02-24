using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Blog.Backend.DataAccess.Entities.Objects
{
    public class EducationType
    {
        [Key]
        public int EducationTypeId { get; set; }
        public string EducationTypeName { get; set; }
        public virtual ICollection<Education> Education { get; set; }
    }
}
