using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Blog.Web.Api.Models
{
    public class ErrorLogModel
    {
        public string ErrorUrl { get; set; }
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public string Cause { get; set; }
    }
}