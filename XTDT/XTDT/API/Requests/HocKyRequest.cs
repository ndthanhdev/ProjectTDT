using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Requests
{
    public class HocKyRequest : AuthRequestObject
    {
        public override string act => "tkb";
        public string option => "ln";
        public int id { get; set; }
    }
}
