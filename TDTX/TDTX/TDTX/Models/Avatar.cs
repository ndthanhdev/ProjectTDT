using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using TDTX.Base;
using TDTX.Base.API;

namespace TDTX.Models
{
    public class Avatar : BasicApiObject
    {
        public string name { get; set; }
        public string src { get; set; }
        public override string act => "avatar";
    }
}


