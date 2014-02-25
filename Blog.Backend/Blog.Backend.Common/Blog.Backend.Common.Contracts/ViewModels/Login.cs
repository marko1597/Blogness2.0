using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

using System.Text;
using System.Threading.Tasks;

namespace Blog.Backend.Common.Contracts.ViewModels
{
    [DataContract]
    public class Login
    {
        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool RememberMe { get; set; }
    }
}
