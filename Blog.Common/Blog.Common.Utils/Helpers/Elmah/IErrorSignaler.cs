using System;

namespace Blog.Common.Utils.Helpers.Elmah
{
    public interface IErrorSignaler
    {
        void SignalFromCurrentContext(Exception e);
    }
}
