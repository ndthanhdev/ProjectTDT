using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class DSThongBaoRequest : IRequestWithToken
    {
        public string act => "tb";
        [RequestParameter(Name = "user")]
        public string User { get; private set; }

        [RequestParameter(Name = "page")]
        public int Page { get; private set; }

        public DSThongBaoRequest(string user, int page = 1)
        {
            this.User = user;
            this.Page = page;
        }

    }
}
