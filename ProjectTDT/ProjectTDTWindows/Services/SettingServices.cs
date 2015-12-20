using System;
using System.Collections.Generic;
using System.Text;
using Windows.Foundation.Collections;
using ProjectTDTWindows.Common;

namespace ProjectTDTWindows.Services
{
    public class SettingServices
    {
        public static bool IsLogged
        {
            set
            {
                SetState(value);
            }
            get
            {
                return GetLoggedState();
            }
        }

        public static string NameOfUser
        {
            set
            {
                Windows.Storage.ApplicationData.Current.LocalSettings.Values[LocalSettingNames.NameOfUser] = value;

            }
            get
            {
                if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(LocalSettingNames.NameOfUser))
                {
                    return Windows.Storage.ApplicationData.Current.LocalSettings.Values[LocalSettingNames.NameOfUser].ToString();
                }
                else
                    return string.Empty;
            }
        }

        private static bool GetLoggedState()
        {
            if (Windows.Storage.ApplicationData.Current.LocalSettings.Values.ContainsKey(LocalSettingNames.IsLogged))
            {
                if (Convert.ToBoolean(Windows.Storage.ApplicationData.Current.LocalSettings.Values[LocalSettingNames.IsLogged]) == true)
                    return true;
            }

            return false;
        }
        private static void SetState(bool IsLogged)
        {
            Windows.Storage.ApplicationData.Current.LocalSettings.Values[LocalSettingNames.IsLogged] = IsLogged;
        }



    }
}
