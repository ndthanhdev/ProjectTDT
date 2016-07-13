using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Models;

namespace TDTX.Services
{
    public class Transporter
    {
        public static readonly string Host = "trautre.azurewebsites.net/api.php";

        public async Task Transport()
        {
            HttpClient client = new HttpClient();
            await Task.Yield();
            client.Dispose();

        }

        public static async Task<string> GetString(string link)
        {
            await Task.Yield();
            HttpClient client = new HttpClient();
            return await client.GetStringAsync(Host + link);
        }
    }
}
