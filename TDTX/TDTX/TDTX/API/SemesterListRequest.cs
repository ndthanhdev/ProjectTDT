using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TDTX.Models;

namespace TDTX.API
{
    public class SemesterListRequest : BasicRequestObject
    {
        public override string act => "tkb";

        public string option => "lhk";
    }
}
