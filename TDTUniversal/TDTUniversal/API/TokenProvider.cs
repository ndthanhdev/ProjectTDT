using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API
{
    public class TokenProvider
    {
        public string User { get; private set; }
        public string Password { get; private set; }
        public TokenProvider(string user, string pass)
        {
            this.User = user;
            this.Password = pass;
        }
        public async Task<string> GetTokenAsync()
        {
            TDTServices.AuthServiceSoapClient client = new TDTServices.AuthServiceSoapClient();
            var sv = await client.AuthenticateAsync(User, Password);
            return sv?.Token;
        }
    }
}
