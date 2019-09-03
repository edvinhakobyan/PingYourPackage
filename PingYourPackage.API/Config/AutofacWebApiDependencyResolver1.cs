using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dependencies;

namespace PingYourPackage.API.Config
{
    public class AutofacWebApiDependencyResolver : IDependencyResolver, IDependencyScope, IDisposable
    {

        public ILifetimeScope _ILifetimeScope { get; }

        public AutofacWebApiDependencyResolver(IContainer container)
        {
            _ILifetimeScope = container.BeginLifetimeScope();
        }

        public IDependencyScope BeginScope()
        {
            return this;
        }

        public void Dispose()
        {
            _ILifetimeScope.Dispose();
        }

        public object GetService(Type serviceType)
        {
            _ILifetimeScope.TryResolve(serviceType, out object instance);
            return instance;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            var ienumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
            _ILifetimeScope.TryResolve(ienumerableType, out object instance);
            return (IEnumerable<object>)instance;
        }
    }
}
