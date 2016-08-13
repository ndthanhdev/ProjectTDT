using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Respond
{
    public class Avartar
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("src")]
        public string src { get; set; }
    }
}
