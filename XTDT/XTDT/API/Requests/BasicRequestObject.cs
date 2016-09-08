using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using XTDT.API.Attributes;
using XTDT.TDTService;

namespace XTDT.API.Requests
{
    public abstract class AuthRequestObject : RequestObject
    {
        public string user { get; set; }

        public string pass { get; set; }

        public abstract string act { get; }

        public override string GenerateQuery()
        {
            return base.GenerateQuery();// GenerateQueryAsync().Result;
        }

        public async Task<string> GenerateQueryAsync()
        {
            try
            {
                AuthServiceSoapClient client = new AuthServiceSoapClient();
                var sv = await client.AuthenticateAsync(user, pass);
                if (sv == null)
                    return string.Empty;
                return base.GenerateQuery() + "&token=" + sv.Token;
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
