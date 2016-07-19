using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.API
{
    public class SemesterRequest : BasicRequestObject
    {
        public override string act => "tkb";
        public string option => "ln";
        public string id => "83";
    }
}
