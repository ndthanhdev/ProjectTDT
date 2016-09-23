using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class ThongBaoContentRequest : IRequestWithToken
    {
        public string act => "tb";

        [RequestParameter(Name = "user")]
        public string User { get; private set; }

        [RequestParameter(Name = "id")]
        public string ViewId { get; private set; }

        public ThongBaoContentRequest(string user, string viewId)
        {
            this.User = user;
            this.ViewId = viewId;
        }

    }
}
