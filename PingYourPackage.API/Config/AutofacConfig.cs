using Autofac;
using Autofac.Integration.WebApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PingYourPackage.API.Config
{
    public class AutofacConfig
    {
        public static void Initialize(HttpConfiguration config)
        {
            IContainer container = RegisterServicesControllers();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        /// <summary>
        /// Register types that implement System.Web.Http.Controllers.IHttpController in the ExecutingAssembly.
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        private static IContainer RegisterServicesControllers()
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            IContainer container = containerBuilder.Build();
            return container;
        }
    }
}
