using System;
using System.Collections.Generic;
using System.Text;

namespace XTDT.API.Requests
{
    public class DSThongBaoRequest : AuthRequestObject
    {
        public override string act => "tb";
        public string lv { get; set; }
        public int page { get; set; }
    }
}
