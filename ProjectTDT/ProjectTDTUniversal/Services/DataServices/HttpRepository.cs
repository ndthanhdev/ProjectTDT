using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Services.DataServices
{
    public  class HttpRepository
    {
        static HttpRepository Instance;

        public static string Content { get { return Instance._content; } set { Instance._content = value; } }

        private string _content;
        static HttpRepository()
        {
            Instance = Instance ?? new HttpRepository();
        }

        private HttpRepository()
        {
            
        }
            
    }
}
