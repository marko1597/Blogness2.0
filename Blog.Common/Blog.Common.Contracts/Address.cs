﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Address : BaseObject
    {
        [DataMember]
        public int AddressId { get; set; }

        [DataMember]
        [Required]
        public int UserId { get; set; }

        [DataMember]
        [Required]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [DataMember]
        [Required]
        public string City { get; set; }

        [DataMember]
        [Required]
        public string State { get; set; }

        [DataMember]
        [Display(Name = "Zip Code")]
        public int? Zip { get; set; }

        [DataMember]
        [Required]
        public string Country { get; set; }
    }
}
