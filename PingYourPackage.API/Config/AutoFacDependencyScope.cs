using System;
using System.Collections.Generic;
using System.Web.Http.Dependencies;
using Autofac;

namespace PingYourPackage.API.Config
{
    internal class AutoFacDependencyScope : IDependencyScope
    {
        private readonly ILifetimeScope _lifetimeScope;

        public AutoFacDependencyScope(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public void Dispose()
        {
            _lifetimeScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            _lifetimeScope.TryResolve(serviceType, out object instance);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var ienumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _lifetimeScope.TryResolve(ienumerableType, out object instance);
            return (IEnumerable<object>)instance;
        }
    }
}