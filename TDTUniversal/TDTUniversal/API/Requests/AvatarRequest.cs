using System;
using System.Collections.Generic;
using System.Text;
using TDTUniversal.API.Attributes;
using TDTUniversal.API.Interfaces;

namespace TDTUniversal.API.Requests
{
    public class AvatarRequest : IRequestWithToken
    {
        public string act => "avatar";

        [RequestParameter(Name = "user")]
        public string User { get; }

        [RequestParameter(Name = "pass")]
        public string Password { get; }

        public AvatarRequest(string user, string password)
        {
            this.User = user;
            this.Password = password;
        }
    }
}
