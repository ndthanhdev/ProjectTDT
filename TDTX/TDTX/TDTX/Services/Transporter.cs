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
        public static async Task<TransportRespond> Transport(ApiObject request)
        {
            await Task.Yield();
            TransportRespond respond = new TransportRespond();
            try
            {
                string content = await GetString(request.Query);
                if (Newtonsoft.Json.Linq.JObject.Parse(content)["error"] == null)
                {
                    //TODO login fail
                }

            }
            catch (HttpRequestException)
            {
                respond.Content = request;
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
