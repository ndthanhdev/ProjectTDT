using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class DSMonHocRequest : IRequestWithToken
    {

        public string act => "tkb";
        public string option => "ln";
        [RequestParameter(Name = "id")]
        public int Id { get; set; }
        public DSMonHocRequest(int id)
        {
            Id = id;
        }

    }
}
