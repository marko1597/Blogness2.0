using System;

namespace Blog.Common.Web.Extensions.Elmah
{
    public interface IErrorSignaler
    {
        void SignalFromCurrentContext(Exception e);
    }
}
