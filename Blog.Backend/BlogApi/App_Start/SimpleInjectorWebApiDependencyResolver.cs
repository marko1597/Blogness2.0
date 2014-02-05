using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;

using SimpleInjector;
using SimpleInjector.Extensions.LifetimeScoping;

namespace BlogApi.App_Start
{
    public class SimpleInjectorWebApiDependencyResolver : IDependencyResolver
    {
        private readonly Container _container;
        private readonly LifetimeScope _lifetimeScope;

        public SimpleInjectorWebApiDependencyResolver(Container container) : this(container, false)
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
                _lifetimeScope.Dispose();
            }
        }
    }
}