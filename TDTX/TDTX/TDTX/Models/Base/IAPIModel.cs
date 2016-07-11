using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TDTX.Models.Base
{
    interface IAPIModel
    {
        [JsonIgnore]
        RESTObject RestObject { get; }
    }
}
