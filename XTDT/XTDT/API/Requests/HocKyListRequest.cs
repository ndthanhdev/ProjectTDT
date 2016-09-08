using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Requests
{
    public class HocKyListRequest : AuthRequestObject
    {
        public override string act => "tkb";

        public string option => "lhk";
    }
}
