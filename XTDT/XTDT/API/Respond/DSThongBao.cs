using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class DonVi
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class ThongBao
    {
        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }

    public class DSThongBao
    {
        [JsonProperty("donvi")]
        public IList<DonVi> DonVi { get; set; }

        [JsonProperty("thongbao")]
        public IList<ThongBao> Thongbao { get; set; }

        [JsonProperty("numpage")]
        public string Numpage { get; set; }
    }

}
