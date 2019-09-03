using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace PingYourPackage.API
{
    public class GuidRouteConstraint : IHttpRouteConstraint
    {
        private const string _format = "D";
        public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, 
                          IDictionary<string, object> values, HttpRouteDirection routeDirection)
        {
            if (values[parameterName] != RouteParameter.Optional)
            {
                object val;
                values.TryGetValue(parameterName, out val);
                string input = Convert.ToString(val, CultureInfo.InvariantCulture);
                Guid guidValue;
                return Guid.TryParseExact(input, _format, out guidValue);
            }
            return true;
        }
    }
}
