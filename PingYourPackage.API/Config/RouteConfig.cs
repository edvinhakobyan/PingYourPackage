using System.Web.Http;

namespace PingYourPackage.API.Config
{
    public class RouteConfig
    {
        public static void RegisterRoutes(HttpRouteCollection routeCollection)
        {
            routeCollection.MapHttpRoute(name: "DefaultHttpRoute",
                                         routeTemplate: "api/{controller}/{key}",
                                         defaults: new { key = RouteParameter.Optional },
                                         constraints: new { key = new GuidRouteConstraint() });
        }

    }
}
