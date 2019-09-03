using Autofac;
using Autofac.Integration.WebApi;
using PingYourPackage.Domain.Entitys.Core;
using PingYourPackage.Domain.Services;
using PingYourPackage.Domain.Services.Interfaces;
using System.Data.Entity;
using System.Reflection;

namespace PingYourPackage.API.Config
{
    public class AutofacWebAPI
    {
        // Lines removed for brevity
        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //EF DbContext
            builder.RegisterType<EntitiesContext>().As<DbContext>().InstancePerApiRequest();

            //Repositories
            builder.RegisterGeneric(typeof(IEntityRepository<>)).As(typeof(IEntityRepository<>)).InstancePerApiRequest();

            //Services
            builder.RegisterType<CryptoService>().As<ICryptoService>().InstancePerApiRequest();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerApiRequest();
            builder.RegisterType<ShipmentService>().As<IShipmentService>().InstancePerApiRequest();

            return builder.Build();
        }

       






    }







    //public class AutofacConfig
    //{
    //    public static void Initialize(HttpConfiguration config)
    //    {
    //        IContainer container = RegisterServicesControllers();
    //        config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
    //    }

    //    /// <summary>
    //    /// Register types that implement System.Web.Http.Controllers.IHttpController in the ExecutingAssembly.
    //    /// </summary>
    //    /// <param name="builder"></param>
    //    /// <returns></returns>
    //    private static IContainer RegisterServicesControllers()
    //    {
    //        ContainerBuilder containerBuilder = new ContainerBuilder();
    //        containerBuilder.RegisterApiControllers(Assembly.GetExecutingAssembly());
    //        IContainer container = containerBuilder.Build();
    //        return container;
    //    }
    //}
}
