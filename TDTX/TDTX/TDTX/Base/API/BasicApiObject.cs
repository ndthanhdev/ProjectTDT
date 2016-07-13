using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Base;
using TDTX.Base.API;

namespace TDTX.Services.API
{
    public abstract class BasicApiObject:ApiObject
    {
        [RequestProperty]
        public string user { get; set; }

        [RequestProperty]
        public string pass { get; set; }

        [RequestProperty]
        public abstract string act { get; }
    }
}
