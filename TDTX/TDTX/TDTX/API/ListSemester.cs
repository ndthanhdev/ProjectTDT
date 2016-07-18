using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using TDTX.Models;

namespace TDTX.API
{
    public class ListSemester : BasicRequestObject
    {
        public override string act => "tkb";

        [RequestProperty]
        public string option => "lhk";
    }
}
