using System;
using System.Web;
using Elmah;

namespace Blog.Common.Web.Extensions.Elmah
{
    public class ErrorSignaler : IErrorSignaler
    {
        public void SignalFromCurrentContext(Exception e)
        {
            if (HttpContext.Current != null)
                ErrorSignal.FromCurrentContext().Raise(e);
        }
    }
}
