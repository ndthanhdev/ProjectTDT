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
using TDTX.Base.API;
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
        public static async Task<TransportRespond<T>> Transport<T>(T request) where T : ApiObject
        {
            await Task.Yield();
            TransportRespond<T> respond = new TransportRespond<T>();
            respond.Content = request;
            try
            {
                string content = await GetString(request.Query);
                JProperty jp = JObject.Parse(content).Property("error");

                if (jp != null)
                {
                    //Login Failed
                    if (jp.Value.ToString() == "Login Failed")
                    {
                        respond.Status = TransportStatusCode.NotAuthorized;
                    }
                    else //UnknownError
                    {
                        respond.Status = TransportStatusCode.UnknownError;
                    }
                }
                else//well transport
                {
                    respond.Status = TransportStatusCode.OK;
                    respond.Content = JsonConvert.DeserializeObject<T>(content);
                }

            }
            catch (HttpRequestException) //Offline
            {
                respond.Status = TransportStatusCode.Offline;
            }
            catch (Exception)
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
