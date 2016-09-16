using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.Services
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

        public string StudentID
        {
            get
            {
                string result = _helper.Read<string>(nameof(StudentID), string.Empty);
                return result;
            }
            set
            {
                _helper.Write<string>(nameof(StudentID), value);
            }
        }

        public string Password
        {
            get
            {
                string result = _helper.Read<string>(nameof(Password), string.Empty);
                return result;
            }
            set
            {
                _helper.Write<string>(nameof(Password), value);
            }
        }

        public string Avatar
        {
            get
            {
                string result = _helper.Read<string>(nameof(Avatar), string.Empty);
                return result;
            }
            set
            {
                _helper.Write<string>(nameof(Avatar), value);
            }
        }
        public string Name
        {
            get
            {
                string result = _helper.Read<string>(nameof(Name), "Sinh viên");
                return result;
            }
            set
            {
                _helper.Write<string>(nameof(Name), value);
            }
        }
    }
}


