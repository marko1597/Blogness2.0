using System;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace Blog.Frontend.Common.Helper
{
    public class ServiceProxyHelper<TInterface> : ClientBase<TInterface>, IDisposable where TInterface : class
    {
        /// <summary>
        /// The service proxy delegate.
        /// </summary>
        /// <param name="proxy">The proxy interface.</param>
        public delegate void ServiceProxyDelegate(TInterface proxy);

        /// <summary>
        /// Create a new instance of <see cref="ServiceProxyHelper{TInsterface}" />.
        /// </summary>
        public ServiceProxyHelper()
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="ServiceProxyHelper{TInsterface}" />.
        /// </summary>
        /// <param name="endpointConfigurationName">The end-point configuration name.</param>
        public ServiceProxyHelper(string endpointConfigurationName)
            : base(endpointConfigurationName)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="ServiceProxyHelper{TInsterface}" />.
        /// </summary>
        /// <param name="endpointConfigurationName">The end-point configuration name.</param>
        /// <param name="remoteAddress">The remote address to use</param>
        public ServiceProxyHelper(string endpointConfigurationName, string remoteAddress)
            : base(endpointConfigurationName, remoteAddress)
        {
        }

        /// <summary>
        /// Create a new instance of <see cref="ServiceProxyHelper{TInsterface}" />.
        /// </summary>
        /// <param name="binding">The binding method.</param>
        /// <param name="remoteAddress">The remote address.</param>
        public ServiceProxyHelper(Binding binding, EndpointAddress remoteAddress)
            : base(binding, remoteAddress)
        {
        }

        /// <summary>
        /// Gets the proxy chanel.
        /// </summary>
        public TInterface Proxy
        {
            get { return Channel; }
        }

        /// <summary>
        /// Forcefully disposes this instance.
        /// </summary>
        public void Dispose()
        {
            if (State == CommunicationState.Faulted)
                Abort();
            else
                try { Close(); }
                catch { Abort(); }
        }

        /// <summary>
        /// Invokes a delagate from proxy.
        /// </summary>
        /// <param name="proxyDelegate">The delegate.</param>
        public static void Call(ServiceProxyDelegate proxyDelegate)
        {
            Call(proxyDelegate, typeof(TInterface).ToString());
        }

        /// <summary>
        /// Invokes a delagate from proxy.
        /// </summary>
        /// <param name="proxyDelegate">The delegate.</param>
        /// <param name="endpointConfigurationName">The end-point configuration name.</param>
        public static void Call(ServiceProxyDelegate proxyDelegate, string endpointConfigurationName)
        {
            var channel = new ChannelFactory<TInterface>(endpointConfigurationName);


            try
            {
                proxyDelegate(channel.CreateChannel());
            }
            finally
            {
                if (channel.State == CommunicationState.Faulted)
                    channel.Abort();
                else
                    try { channel.Close(); }
                    catch { channel.Abort(); }
            }
        }
    }
}
