using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts
{
    [DataContract]
    public class Address
    {
        [DataMember]
        public int AddressId { get; set; }

        [DataMember]
        public User User { get; set; }

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
