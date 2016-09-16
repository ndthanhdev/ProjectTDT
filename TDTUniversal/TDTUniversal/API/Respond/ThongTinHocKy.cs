using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class ThongTinHocKy
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("TenHocKy")]
        public string TenHocKy { get; set; }
    }
}
