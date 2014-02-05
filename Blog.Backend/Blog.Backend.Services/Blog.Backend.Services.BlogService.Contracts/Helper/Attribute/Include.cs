using System;

namespace Blog.Backend.Services.BlogService.Contracts.Helper.Attribute
{
    [AttributeUsage(AttributeTargets.Class|AttributeTargets.Struct|AttributeTargets.Property, AllowMultiple=true)]
    public class Include : System.Attribute
    {
        public Include(bool isIncluded) { IsIncluded = isIncluded; }
        public bool IsIncluded;
    }
}
