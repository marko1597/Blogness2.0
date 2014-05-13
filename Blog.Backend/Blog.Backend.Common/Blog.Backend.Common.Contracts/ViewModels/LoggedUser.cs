﻿using System.Runtime.Serialization;

namespace Blog.Backend.Common.Contracts.ViewModels
{
    [DataContract]
    public class LoggedUser : BaseContract
    {
        [DataMember]
        public User User { get; set; }

        [DataMember]
        public Session Session { get; set; }
    }
}
