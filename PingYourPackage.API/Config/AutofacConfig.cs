using Autofac;
using Autofac.Integration.WebApi;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

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
