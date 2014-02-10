using System;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.BlogObjects
{
    [DataContract]
    public class Session
    {
        [DataMember]
        public int SessionId { get; set; }

        [DataMember]
        public string Token { get; set; }

        [DataMember]
        public int UserId { get; set; }

        [DataMember]
        public string IpAddress { get; set; }

        [DataMember]
        public DateTime TimeValidity { get; set; }
    }
}
