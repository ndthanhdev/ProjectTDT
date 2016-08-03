using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TDTX
{
    /// <summary>
    /// this part save specific constant for each platform
    /// </summary>
    public partial class App : Application
    {
        public static readonly string NameSpace = Device.OnPlatform(
            Android: "TDTX.Android",
            iOS: "TDTX.iOS",
            WinPhone: "TDTX.UWP");

        public static readonly string SettingsKey = "Setting.txt";

        public static readonly string TimeTableKey = "TimeTable.txt";

        public static readonly string Host = "http://trautre.azurewebsites.net/api.php?";

    }
}
