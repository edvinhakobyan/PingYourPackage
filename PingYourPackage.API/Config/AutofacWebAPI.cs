using Autofac;
using PingYourPackage.Domain.Entitys.Core;
using PingYourPackage.Domain.Services;
using PingYourPackage.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Dependencies;

namespace PingYourPackage.API.Config
{
    public class AutofacWebAPI
    {
        public static void Initialize(HttpConfiguration config)
        {
            IContainer container = RegisterServices();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }


        private static IContainer RegisterServices()
        {
            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            //EF DbContext
            builder.RegisterType<EntitiesContext>().As<DbContext>().InstancePerRequest();

            //Repositories
            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IEntityRepository<>)).InstancePerRequest();

            //Services
            builder.RegisterType<CryptoService>().As<ICryptoService>().InstancePerRequest();
            builder.RegisterType<MembershipService>().As<IMembershipService>().InstancePerRequest();
            builder.RegisterType<ShipmentService>().As<IShipmentService>().InstancePerRequest();

            return builder.Build();
        }
    }
}
