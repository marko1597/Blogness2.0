using System.Collections.Generic;
using System.Runtime.Serialization;
using Blog.Backend.Services.BlogService.Contracts.BlogObjects;

namespace Blog.Backend.Services.BlogService.Contracts.ViewModels
{
    [DataContract]
    public class UserMediaGroup
    {
        [DataMember]
        public int MediaGroupId { get; set; }

        [DataMember]
        public string MediaGroupName { get; set; }

        [DataMember]
        public List<Media> Media { get; set; }
    }
}
