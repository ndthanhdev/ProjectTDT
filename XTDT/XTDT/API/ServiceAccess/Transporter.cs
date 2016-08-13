using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using XTDT.API.Material;
using XTDT.API.Requests;

namespace XTDT.API.ServiceAccess
{
    public static class Transporter
    {
        public static string Host = "http://trautre.azurewebsites.net/api.php?";
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
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(Host + query);
        }

    }
}
