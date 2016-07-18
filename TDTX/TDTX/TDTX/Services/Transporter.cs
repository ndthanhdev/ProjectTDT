using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using TDTX.API;
using TDTX.Models;


namespace TDTX.Services
{
    public static class Transporter
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="requester"></param>
        /// <returns>true if completed</returns>
        public static async Task<TransportRespond<T,U>> Transport<T,U>(T request) where T:RequestObject
        {
            await Task.Yield();
            TransportRespond<T,U> respond = new TransportRespond<T,U>();
            respond.Request = request;
            try
            {
                string content = await GetString(request.Query);
                JToken token = JToken.Parse(content);
                if (token == null)
                {
                    respond.Status = TransportStatusCode.UnknownError;
                }
                else //well transport
                {
                    if (token.Type == JTokenType.Object)
                    {
                        JProperty jp = JObject.Parse(content).Property("error");
                        //Login Failed
                        if (jp.Value.ToString() == "Login Failed")
                        {
                            respond.Status = TransportStatusCode.NotAuthorized;
                            goto EndPoint;
                        }
                        else //UnknownError
                        {
                            respond.Status = TransportStatusCode.UnknownError;
                            goto EndPoint;
                        }
                    }
                    respond.Status = TransportStatusCode.OK;
                    //can replace with populate
                    U respondContent = JsonConvert.DeserializeObject<U>(content);
                    respond.Respond = respondContent;
                }
                EndPoint:
                await Task.Yield();
            }
            catch (HttpRequestException) //Offline
            {
                respond.Status = TransportStatusCode.Offline;
            }
            catch (Exception e)
            {
                respond.Status = TransportStatusCode.UnknownError;
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
            return await client.GetStringAsync(App.Host + query);
        }

    }
}
