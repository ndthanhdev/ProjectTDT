using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TDTX.Base;
using TDTX.Base.API;
using TDTX.Services.API;
using TDTX.Services.API.Base;

namespace TDTX.Models
{
    public class Avatar : ApiObject
    {
        public string name { get; set; }
        public string src { get; set; }

        public RequestObject Request { get; set; }

        [RequestProperty]
        public string user { get; set; }

    }
}


