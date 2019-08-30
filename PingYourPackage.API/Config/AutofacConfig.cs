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
    class AutofacConfig
    {
        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            return builder.Build();
        }

        public static void Initialize(HttpConfiguration config)
        {
            ContainerBuilder containerBuilder = new ContainerBuilder();
            Initialize(config, RegisterServices(containerBuilder));
        }
    }
}
