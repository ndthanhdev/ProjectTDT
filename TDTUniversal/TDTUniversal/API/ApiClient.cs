using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TDTUniversal.API
{
    public class ApiClient
    {
        public static async Task<Package<T, U>> GetAsync<T, U>(T request, TokenProvider tokenProvider = null)
        {
            Package<T, U> respond = new Package<T, U>();
            respond.Request = request;
            try
            {
                var url = await RequestBuilder.BuildUrl(request, tokenProvider);
                string content = await ApiClient.GetString(url);
                var box = JsonConvert.DeserializeObject<Wrapper<U>>(content,
                       new IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy" });
                respond.Status = box.Status;
                if (!respond.Status)
                {                   
                    return respond;
                }
                respond.Respond = box.Data;
            }
            catch (Exception ex)
            {
                respond.Status = false;
            }
            return respond;
        }

        public async static Task<string> GetString(string url)
        {
            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (HttpClient client = new HttpClient(handler))
                {
                    var html = await client.GetStringAsync(url);
                    var text = WebUtility.HtmlDecode(html);
                    Regex regex = new Regex(@"\s{2,}");
                    var result = regex.Replace(text, string.Empty);
                    return result;
                }
            }
        }

        class Wrapper<T>
        {
            [JsonProperty("status")]
            public bool Status { get; set; }

            [JsonProperty("data")]
            public T Data { get; set; }
        }
    }
}
