using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class Tkb
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
        public Lich[] Lich { get; set; }
    }


}
