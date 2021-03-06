﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class UserPosts : BaseObject
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public List<Post> Posts { get; set; }
    }
}
