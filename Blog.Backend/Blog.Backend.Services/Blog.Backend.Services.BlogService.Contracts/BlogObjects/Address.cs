using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class Address
    {
        [Key]
        [DataMember]
        public int AddressId { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string StreetAddress { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public int? Zip { get; set; }

        [DataMember]
        public string Country { get; set; }
    }
}
