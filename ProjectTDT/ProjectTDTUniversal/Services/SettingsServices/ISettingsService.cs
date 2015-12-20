using System;
using Windows.UI.Xaml;

namespace ProjectTDTUniversal.Services.SettingsServices
{
    public interface ISettingsService
    {
        bool UseShellBackButton { get; set; }
        ApplicationTheme AppTheme { get; set; }
        TimeSpan CacheMaxDuration { get; set; }

        //app code

            /// <summary>
            /// logged is true, guest is false
            /// </summary>
        bool LoginStatus { get; set; }

        string UserName { get; set; }
    }
}
