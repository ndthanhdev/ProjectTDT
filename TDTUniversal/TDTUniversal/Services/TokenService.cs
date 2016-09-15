using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.API;
using TDTUniversal.Services.SettingsServices;

namespace TDTUniversal.Services
{
    public class TokenService
    {
        public static TokenService Instance { get; private set; } = new TokenService();

        public TokenProvider TokenProvider { get; private set; }

        public TokenService()
        {
            TokenProvider = new TokenProvider(SettingsService.Instance.User, SettingsService.Instance.Password);
            Instance = this;
        }


    }
}
