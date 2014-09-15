using System;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Elmah;

namespace Blog.Common.Utils.Helpers.Elmah
{
    [ExcludeFromCodeCoverage]
    public class ErrorSignaler : IErrorSignaler
    {
        public void SignalFromCurrentContext(Exception e)
        {
            if (HttpContext.Current != null)
            {
                ErrorSignal.FromCurrentContext().Raise(e);
            }
            else
            {
                ErrorLog.GetDefault(null).Log(new Error(e));
            }
        }
    }
}
