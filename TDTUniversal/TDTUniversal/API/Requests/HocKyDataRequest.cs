using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class HocKyDataRequest : IRequestWithToken
    {

        public string act => "tkb";
        public string option => "ln";
        [RequestParameter(Name = "id")]
        public int Id { get; set; }
        [RequestParameter(Name = "user")]
        public string User { get; set; }

        public HocKyDataRequest(int id,string user)
        {
            Id = id;
            User = user;
        }

    }
}
