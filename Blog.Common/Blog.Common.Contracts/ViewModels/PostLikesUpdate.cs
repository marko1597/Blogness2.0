using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Common.Contracts.ViewModels
{
    [DataContract]
    public class PostLikesUpdate
    {
        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        public List<PostLike> PostLikes { get; set; }
    }
}
