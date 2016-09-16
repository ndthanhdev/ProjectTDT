using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class LichHoc
    {
        [JsonProperty("tiet")]
        public string Tiet { get; set; }

        [JsonProperty("thu")]
        public int Thu { get; set; }

        [JsonProperty("phong")]
        public string Phong { get; set; }

        [JsonProperty("tuan")]
        public string Tuan { get; set; }
    }
}
