using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Requests
{
    public class SemesterListRequest : BasicRequestObject
    {
        public override string act => "tkb";

        public string option => "lhk";
    }
}
