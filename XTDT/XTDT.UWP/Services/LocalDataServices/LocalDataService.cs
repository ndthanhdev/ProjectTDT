using System;
using Template10.Common;
using Template10.Utils;
using Windows.UI.Xaml;

namespace XTDT.UWP.Services.LocalDataServices
{
    public class LocalDataService
    {
        static LocalDataService _instance;
        public static LocalDataService Instance => _instance ?? (_instance = new LocalDataService());
        Template10.Services.SettingsService.ISettingsHelper _helper;

        public LocalDataService()
        {
            _helper = new Template10.Services.SettingsService.SettingsHelper();
        }

        public bool IsLogged
        {
            get
            {
                bool result = _helper.Read<bool>(nameof(IsLogged), false);
                return result;
            }
            set
            {
                _helper.Write<bool>(nameof(IsLogged), value);
            }
        }
    }
}

