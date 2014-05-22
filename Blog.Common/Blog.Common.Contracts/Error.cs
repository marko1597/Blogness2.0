using System;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class Error
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}
