﻿using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Blog.Common.Contracts
{
    [DataContract]
    public class PostContent : BaseObject
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int PostId { get; set; }

        [DataMember]
        [Display(Name = "Title")]
        public string PostContentTitle { get; set; }

        [DataMember]
        [Display(Name = "Caption")]
        public string PostContentText { get; set; }

        [DataMember]
        public Media Media { get; set; }
    }
}
