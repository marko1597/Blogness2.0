using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class User
    {
        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        public string Password { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string EmailAddress { get; set; }

        [DataMember]
        public DateTime BirthDate { get; set; }

        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public Media Picture { get; set; }

        [DataMember]
        public Media Background { get; set; }

        [DataMember]
        public List<Education> Education { get; set; }

        [DataMember]
        public List<Hobby> Hobbies { get; set; }
    }
}
