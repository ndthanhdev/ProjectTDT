using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace TDTX
{
    public partial class App : Application
    {
        public static readonly string NameSpace = Device.OnPlatform<string>(
            iOS: "TDTX.iOS",
            Android: "TDTX.Android",
            WinPhone: "TDTX.UWP");

        public void InitializeSetting()
        {
            if (!Current.Properties.ContainsKey(nameof(AppProperties.Dictionary)))
            {
                Current.Properties[nameof(AppProperties.Dictionary)] = new System.Globalization.CultureInfo("en");
            }
        }
    }


}
