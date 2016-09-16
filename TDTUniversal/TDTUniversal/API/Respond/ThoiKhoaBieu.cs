using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class ThoiKhoaBieu
    {
        [JsonProperty("MaMH")]
        public string MaMH { get; set; }

        [JsonProperty("TenMH")]
        public string TenMH { get; set; }

        [JsonProperty("Nhom")]
        public string Nhom { get; set; }

        [JsonProperty("To")]
        public string To { get; set; }

        [JsonProperty("Lich")]
        public LichHoc[] Lich { get; set; }

    }
}
