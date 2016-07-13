using System;
using System.Collections.Generic;
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
        public static async Task Transport<T>(ApiObject requester) where T:ApiObject
        {
            await Task.Yield();
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
