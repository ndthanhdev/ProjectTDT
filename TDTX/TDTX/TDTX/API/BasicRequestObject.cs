using System;
using System.Collections.Generic;
using System.Text;
using TDTX.API;




namespace TDTX.API
{
    public abstract class BasicRequestObject:RequestObject
    {
        [RequestProperty]
        public string user { get; set; }

        [RequestProperty]
        public string pass { get; set; }

        [RequestProperty]
        public abstract string act { get; }
    }
}
