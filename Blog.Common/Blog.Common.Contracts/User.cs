using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class User : BaseObject
    {
        [DataMember]
        [Display(Name = "ID")]
        public int Id { get; set; }

        [DataMember]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [DataMember, JsonIgnore]
        [Display(Name = "identity")]
        public string IdentityId { get; set; }

        [DataMember]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [DataMember]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [DataMember]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
        [Display(Name = "Cake Day")]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public Media Picture { get; set; }
        public int? PictureId { get; set; }

        [DataMember]
        public Media Background { get; set; }
        public int? BackgroundId { get; set; }

        [DataMember]
        public bool IsDeleted { get; set; }

        [DataMember]
        public List<Education> Education { get; set; }

        [DataMember]
        public List<Hobby> Hobbies { get; set; }
    }
}
