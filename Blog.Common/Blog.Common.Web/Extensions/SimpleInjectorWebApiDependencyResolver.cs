using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

namespace Blog.Common.Web.Extensions
{
    public class SimpleInjectorWebApiDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;
        private readonly LifetimeScope _lifetimeScope;

        public SimpleInjectorWebApiDependencyResolver(Container container)
            : this(container, false)
        {
        }

        private SimpleInjectorWebApiDependencyResolver(Container container, bool createScope)
        {
            _container = container;

            if (createScope)
            {
                _lifetimeScope = container.BeginLifetimeScope();
            }
        }

        public IDependencyScope BeginScope()
        {
            return new SimpleInjectorWebApiDependencyResolver(_container, true);
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)_container)
                .GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType);
        }

        public void Dispose()
        {
            if (_lifetimeScope != null)
            {
                /* Code here should be removed because disposing of scope within a different
                 * thread that called it is not advisable due to the asynchronous nature 
                 * of Web Apis.
                 * ========================================================================= */
                //_lifetimeScope.Dispose();
            }
        }
    }
}
