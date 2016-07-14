using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
        public static async Task<bool> Transport(ApiObject requester)
        {
            await Task.Yield();
            try
            {
                return true;

            }
            catch (HttpRequestException)
            {
                return false;

            }
            catch (Exception)
            {
                throw;
            }
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
