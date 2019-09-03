using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.Validation;
using System.Web.Http.Validation.Providers;

namespace PingYourPackage.API.Config
{
    public class WebAPIConfig
    {
        public static void Configure(HttpConfiguration config)
        {
            var jqueryFormatter = config.Formatters.FirstOrDefault(t => t.GetType() == typeof(JQueryMvcFormUrlEncodedFormatter));
            config.Formatters.Remove(config.Formatters.FormUrlEncodedFormatter);
            config.Formatters.Remove(jqueryFormatter);

            foreach (var formater in config.Formatters)
            {
                formater.RequiredMemberSelector = new SuppressedRequiredMemberSelector();
            }

            //Default Services
            config.Services.Replace(typeof(IContentNegotiator), new DefaultContentNegotiator(true));

            config.Services.RemoveAll(typeof(ModelValidatorProvider),   validator => !(validator is DataAnnotationsModelValidatorProvider));

        }

    }
}
