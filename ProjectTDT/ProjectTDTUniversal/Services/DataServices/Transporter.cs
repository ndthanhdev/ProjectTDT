using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Web.Http;
using Windows.Web.Http.Headers;
using Windows.Web.Http.Filters;
using Windows.Security.Credentials;
using ProjectTDTUniversal.Common;

namespace ProjectTDTUniversal.Services.DataServices
{
    public partial class Transporter
    {
        public static Transporter Instance { get; }

        public static HttpClient Porter
        { get { return Instance.porter; } }



        private HttpClient porter;

        static Transporter()
        {
            Instance = Instance ?? new Transporter();
        }

        private Transporter()
        {
            
            porter = new HttpClient(new PlugInFilter());          

            //fix it
            porter.DefaultRequestHeaders.UserAgent.TryParseAdd("ProjectTDT 2.0");
           

        } 

        public async Task<string> Transport(HttpForm form,params string[] args)
        {
            HttpRequestMessage request = new HttpRequestMessage(form.Method, form.Link);
            request.Content = form.FillIn(args);
            

            HttpResponseMessage response = await porter.SendRequestAsync(request);

            var buffer = await response.Content.ReadAsBufferAsync();
            HttpRepository.Content = Encoding.UTF8.GetString(buffer.ToArray());

            return HttpRepository.Content;
        }
    }


}
