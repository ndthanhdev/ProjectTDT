using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XTDT.API.Material;
using XTDT.API.Requests;

namespace XTDT.API.ServiceAccess
{
    public static class Transporter
    {
        public static string Host = "http://api.trautre.cf/api.php?";
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requester"></param>
        /// <returns>true if completed</returns>
        public static async Task<Package<T, U>> Transport<T, U>(T request) where T : RequestObject
        {
            await Task.Yield();
            Package<T, U> respond = new Package<T, U>();
            respond.Request = request;
            try
            {
                string content = await GetString(request.Query);
                JToken token = JToken.Parse(content);
                if (token == null)
                {
                    respond.Status = PackageStatusCode.UnknownError;
                }
                else //well transport
                {
                    if (token.Type == JTokenType.Object && token["error"] != null)
                    {

                        JProperty jp = JObject.Parse(content).Property("error");
                        //Login Failed
                        if (jp.Value.ToString() == "Login Failed")
                        {
                            respond.Status = PackageStatusCode.NotAuthorized;
                            goto EndPoint;
                        }
                        else //UnknownError
                        {
                            respond.Status = PackageStatusCode.UnknownError;
                            goto EndPoint;
                        }
                    }
                    respond.Status = PackageStatusCode.OK;
                    //can replace with populate
                    U respondContent = JsonConvert.DeserializeObject<U>(content,
                        new IsoDateTimeConverter() { DateTimeFormat = "dd/MM/yyyy" });
                    respond.Respond = respondContent;
                }
                EndPoint:
                await Task.Yield();
            }
            catch (HttpRequestException) //Offline
            {
                respond.Status = PackageStatusCode.Offline;
            }
            catch (Exception e)
            {
                respond.Status = PackageStatusCode.UnknownError;
                throw e;
            }
            return respond;
        }
        /// <summary>
        /// get 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static async Task<string> GetString(string query)
        {
            await Task.Yield();

            using (HttpClientHandler handler = new HttpClientHandler())
            {
                handler.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;
                using (HttpClient client = new HttpClient(handler))
                {
                    client.Timeout = TimeSpan.FromSeconds(60);
                    var result = await client.GetStringAsync(Host + query);//Encoding.UTF8.GetString(bytes);
                    return result;
                }
            }
            
            //HttpClient client = new HttpClient(handler);
            //client.DefaultRequestHeaders.Accept.ParseAdd("text/html, application/xhtml+xml, application/json, image/jxr, */*");
            //client.DefaultRequestHeaders.AcceptEncoding.ParseAdd("gzip, deflate");
            //client.DefaultRequestHeaders.AcceptLanguage.ParseAdd("en-US, en; q=0.7, vi; q=0.3");
            //client.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/51.0.2704.79 Safari/537.36 Edge/14.14393");
            //var url = Host + query;
            //var r2 = await client.GetStringAsync(url);
            //var respond = await client.GetAsync(url);
            //var stream = await respond.Content.ReadAsStreamAsync();
            //using (var reader = new StreamReader(stream, Encoding.UTF8))
            //{
            //    string l = reader.ReadToEnd();

            //}


            //var result = Encoding.UTF8.GetString(await respond.Content.ReadAsByteArrayAsync());
            //return result;
        }

    }
}
