using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Requests
{
    public abstract class BasicRequestObject : RequestObject
    {
        public string user { get; set; }

        public string pass { get; set; }

        public abstract string act { get; }
    }
}
