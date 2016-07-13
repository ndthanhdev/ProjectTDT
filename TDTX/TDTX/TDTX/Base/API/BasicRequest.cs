using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Services.API.Base;

namespace TDTX.Services.API
{
    public class BasicRequest:RequestObject
    {
        public string user { get; set; }
        public string pass { get; set; }
        public string act { get; set; }

    }
}
