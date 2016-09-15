using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Respond
{
    public class Avatar
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string src { get; set; }
    }
}
