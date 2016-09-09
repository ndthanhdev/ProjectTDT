using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class DSHocKyRequest : IRequestWithToken
    {
        public string act => "tkb";
        public string option => "lhk";
        [RequestParameter(Name = "user")]
        public string User { get; private set; }

        public DSHocKyRequest(string user)
        {
            this.User = user;
        }

    }
}
