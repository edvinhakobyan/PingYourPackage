using PingYourPackage.API.Config;
using PingYourPackage.Domain.Entitys;
using PingYourPackage.Domain.Entitys.Core;
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