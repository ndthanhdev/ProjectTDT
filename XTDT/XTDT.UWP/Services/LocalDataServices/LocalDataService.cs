using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XTDT.IServices;

namespace XTDT.UWP.Services.LocalDataServices
{
    public class LocalDataService : ILocalDataService
    {
        public static LocalDataService Instance { get; } = new LocalDataService();

        Template10.Services.SettingsService.ISettingsHelper _helper;
        private LocalDataService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public bool IsLogged
        {
            get
            {
                var isLogged = _helper.Read<bool>(nameof(IsLogged), false);
                return isLogged;
            }
            set
            {
                _helper.Write<bool>(nameof(IsLogged), IsLogged);
            }
        }


    }
}
