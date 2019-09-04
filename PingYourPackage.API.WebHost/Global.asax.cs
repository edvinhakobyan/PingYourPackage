using PingYourPackage.API.Config;
using PingYourPackage.Domain.Entitys;
using PingYourPackage.Domain.Entitys.Core;
using PingYourPackage.Domain.Services;
using PingYourPackage.Domain.Services.Interfaces;
using System;
using System.Web;
using System.Web.Http;

namespace PingYourPackage.API.WebHost
{
    public class Global : HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
 
            EFConfig.Initialize();
            var config = GlobalConfiguration.Configuration;
            WebAPIConfig.Configure(config);
            RouteConfig.RegisterRoutes(config.Routes);
            AutofacWebAPI.Initialize(config);

            using (var v = config.DependencyResolver.BeginScope())
            {
               var ey = v.GetService(typeof(IMembershipService)); 
            }



        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}