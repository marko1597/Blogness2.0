using Blog.Backend.Services.BlogService.Contracts.BlogObjects;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Backend.Services.BlogService.Contracts.ViewModels
{
    [DataContract]
    public class UserPosts
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Post> Posts { get; set; }
    }
}
