using System;
using System.ComponentModel.DataAnnotations;

namespace Blog.DataAccess.Database.Entities.Objects
{
    public class Session
    {
        [Key]
        public int SessionId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public string IpAddress { get; set; }
        public DateTime TimeValidity { get; set; }
    }
}
