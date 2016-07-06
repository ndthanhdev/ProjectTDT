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
#if Android
        public static readonly string NameSpace = "TDTX.Android";
#elif IOS
        public static readonly string NameSpace = "TDTX.iOS";
#elif UWP
        public static readonly string NameSpace = "TDTX.UWP";
#endif

    }
}
