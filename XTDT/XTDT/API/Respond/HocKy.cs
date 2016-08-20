using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class HocKy
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("tkb")]
        public IList<Tkb> Tkb { get; set; }
    }
}
