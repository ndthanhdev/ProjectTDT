using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Web.Http.Filters;

namespace ProjectTDTUniversal.Services.DataServices
{
    public class PlugInFilter : IHttpFilter
    {
        private IHttpFilter innerFilter;

        public PlugInFilter()
        {
            HttpBaseProtocolFilter baseFilter = new HttpBaseProtocolFilter();
            baseFilter.AllowAutoRedirect = false;
            baseFilter.AllowUI = false;                       
            this.innerFilter = baseFilter;           
        }

        public IAsyncOperationWithProgress<HttpResponseMessage, HttpProgress> SendRequestAsync(HttpRequestMessage request)
        {
            return AsyncInfo.Run<HttpResponseMessage, HttpProgress>(async (cancellationToken, progress) =>
            {
                HttpResponseMessage response;
                try
                {
                    response = await innerFilter.SendRequestAsync(request).AsTask(cancellationToken, progress);
                }
                catch (Exception e)
                {
                    response = new HttpResponseMessage(HttpStatusCode.ServiceUnavailable);
                    response.ReasonPhrase = e.Message;
                }
                if (response.StatusCode == HttpStatusCode.MovedPermanently                      
                       || response.StatusCode == HttpStatusCode.Found
                       || response.StatusCode == HttpStatusCode.SeeOther                      
                       || response.StatusCode == HttpStatusCode.TemporaryRedirect
                       || (int)response.StatusCode == 308)
                       // add case you want
                {
                    var newRequest = CopyRequest(response.RequestMessage);
                    if (  response.StatusCode == HttpStatusCode.Found
                          || response.StatusCode == HttpStatusCode.SeeOther)
                    {
                        newRequest.Content = null;
                        newRequest.Method = HttpMethod.Get;
                    }

                    newRequest.RequestUri = response.Headers.Location;
                    response = await SendRequestAsync(newRequest).AsTask(cancellationToken, progress);
                }
                return response;
            });
        }

        public void Dispose()
        {
            innerFilter.Dispose();
            GC.SuppressFinalize(this);
        }

        private static HttpRequestMessage CopyRequest(HttpRequestMessage oldRequest)
        {
            var newrequest = new HttpRequestMessage(oldRequest.Method, oldRequest.RequestUri);

            foreach (var header in oldRequest.Headers)
            {
                newrequest.Headers.TryAppendWithoutValidation(header.Key, header.Value);
            }
            foreach (var property in oldRequest.Properties)
            {
                newrequest.Properties.Add(property);
            }
            if (oldRequest.Content != null) newrequest.Content = oldRequest.Content;
            return newrequest;
        }

    }

}
