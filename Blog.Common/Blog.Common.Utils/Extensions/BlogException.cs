using System;
using System.Runtime.Serialization;

namespace Blog.Common.Utils.Extensions
{
    [Serializable]
    public class BlogException : Exception
    {
        public BlogException() { }

        public BlogException(string message)
            : base(message) { }
        
        public BlogException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
