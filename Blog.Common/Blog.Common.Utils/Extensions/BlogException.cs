﻿using System;
using System.Runtime.Serialization;

namespace Blog.Common.Utils.Extensions
{
    [Serializable]
    public class BlogException : Exception
    {
        public BlogException() { }

        public BlogException(string message)
            : base(message) { }

        public BlogException(string format, params object[] args)
            : base(string.Format(format, args)) { }

        public BlogException(string message, Exception innerException)
            : base(message, innerException) { }

        public BlogException(string format, Exception innerException, params object[] args)
            : base(string.Format(format, args), innerException) { }

        protected BlogException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }
    }
}