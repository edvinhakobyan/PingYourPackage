using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Autofac;

namespace PingYourPackage.API.Config
{
    internal class AutofacWebApiDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public AutofacWebApiDependencyResolver(IContainer container)
        {
            _container = container;
        }
        public void Dispose()
        {
            _container.Dispose();
        }
        public object GetService(Type serviceType)
        {
            _container.TryResolve(serviceType, out object instance);
            return instance;
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            var ienumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _container.TryResolve(ienumerableType, out object instance);
            return (IEnumerable<object>)instance;
        }

        public IDependencyScope BeginScope()
        {
            return new AutoFacDependencyScope(_container.BeginLifetimeScope());
        }
    }
}