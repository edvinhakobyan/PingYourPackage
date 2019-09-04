using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PingYourPackage.API
{
    
    class RequireHttpsMessageHandler : DelegatingHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (request.RequestUri.Scheme == Uri.UriSchemeHttps)
            {
                //что сервер отказывается выполнить запрос.
                HttpResponseMessage httpResponseMessage = request.CreateResponse(HttpStatusCode.Forbidden);

                httpResponseMessage.ReasonPhrase = "SSL Required";

                return new Task<HttpResponseMessage>(() => httpResponseMessage); //Task.FromResult(response);
            }

            return base.SendAsync(request, cancellationToken);
        }

    }
}
