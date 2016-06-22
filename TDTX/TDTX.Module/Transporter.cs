using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TDTX.Module
{
    public class Transporter
    {
        //private static Transporter _instance;
        //public Transporter Instance
        //{
        //    get { return _instance = _instance ?? new Transporter(); }
        //}

        public HttpClient Client
        {
            get; private set;
        }
        public Transporter()
        {
            Client = new HttpClient();
            
        }

        public async Task<string> test()
        {
            return await (await Client.GetAsync("https://www.google.com.vn")).Content.ReadAsStringAsync();
        }
    }
}
