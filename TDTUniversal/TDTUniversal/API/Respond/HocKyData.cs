using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class HocKyData
    {
        [JsonProperty("start")]
        public DateTime Start { get; set; }

        [JsonProperty("tkb")]
        public IList<ThoiKhoaBieu> TKB { get; set; }

    }
}
