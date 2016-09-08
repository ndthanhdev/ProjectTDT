using System;
using System.Collections.Generic;
using System.Text;
using TDTUniversal.API.Attributes;

namespace TDTUniversal.API.Requests
{
    public class AvatarRequest 
    {
        public string act => "avatar";

        [RequestParameter(Name ="user")]
        public string User { get; set; }

        [RequestParameter(Name = "pass")]
        public string Password { get; set; }

        [RequestParameter(Name = "token")]
        public string Token { get; set; }

        public AvatarRequest(string user,string password)
        {
            this.User = user;
            this.Password = password;
        }
    }
}
