using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using Blog.Common.Utils.Helpers.Elmah;

namespace Blog.Services.Implementation.Handlers
{
    /// <summary>
    /// This handler will tell ELMAH about exceptions in the WCF service
    /// </summary>
    public class HttpErrorHandler : IErrorHandler
    {
        private IErrorSignaler _errorSignaler;
        public IErrorSignaler ErrorSignaler
        {
            get
            {
                return _errorSignaler ?? new ErrorSignaler();
            }
            set { _errorSignaler = value; }
        }

        public bool HandleError(Exception error)
        {
            return false;
        }

        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error == null) return;
            ErrorSignaler.SignalFromCurrentContext(error);
        }
    }
}