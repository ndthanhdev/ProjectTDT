using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace ProjectTDTWindows.Services
{
    static public class HtmlProvider
    {
        public static async Task<string> GetHtml(string uri)
        {
            try
            {
                if (!Common.InternetConnection.IsInternetAvailable())
                {
                    return "";
                }
                HttpClient Client = new HttpClient(new HttpClientHandler() { AllowAutoRedirect = true });
                Client.DefaultRequestHeaders.Accept.ParseAdd("text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8");
                Client.DefaultRequestHeaders.AcceptCharset.ParseAdd("utf-8");
                return await Client.GetStringAsync(uri);
            }
            catch (Exception ex)
            {
                throw new Exception("Can't get html", ex);
            }

        }

    }
}
