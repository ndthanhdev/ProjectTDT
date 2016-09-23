using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class ThongBaoRaw
    {

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("unread")]
        public bool IsNew { get; set; }
    }

    public class DSThongBao
    {

        [JsonProperty("thongbao")]
        public IList<ThongBaoRaw> Thongbao { get; set; }

        [JsonProperty("numpage")]
        public int Numpage { get; set; }
    }
}
