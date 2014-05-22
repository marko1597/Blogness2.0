using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using SimpleInjector;

namespace Blog.Web.Site
{
    public class SimpleInjectorDependencyResolver : IDependencyResolver
    {
        public Container Container { get; private set; }

        public SimpleInjectorDependencyResolver(Container container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            Container = container;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return ((IServiceProvider)Container).GetService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Container.GetAllInstances(serviceType);
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }
    }
}