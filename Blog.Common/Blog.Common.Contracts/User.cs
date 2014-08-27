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
        public int Id { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember, JsonIgnore]
        public string IdentityId { get; set; }

        [DataMember]
        [Required]
        public string FirstName { get; set; }

        [DataMember]
        [Required]
        public string LastName { get; set; }

        [DataMember]
        [EmailAddress]
        public string EmailAddress { get; set; }

        [DataMember]
        [DataType(DataType.Date)]
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
        public List<Education> Education { get; set; }

        [DataMember]
        public List<Hobby> Hobbies { get; set; }
    }
}
