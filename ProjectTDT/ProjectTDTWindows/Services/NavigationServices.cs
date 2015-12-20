using ProjectTDTWindows;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Windows.System;

namespace ProjectTDTWindows.Services
{
    public class NavigationServices
    {

        static public void NavigateToPage(string pageName, object parameter = null)
        {
            try
            {
                string pageTypeName = "";
                pageTypeName = String.Format("{0}.{1}", typeof(ProjectTDTWindows.Views.MainPage).Namespace, pageName);
                Type pageType = Type.GetType(pageTypeName);
                App.RootFrame.Navigate(pageType, parameter);



            }
            catch (Exception ex)
            {
                // AppLogs.WriteError("NavigationServices.NavigateToPage", ex);
                Debug.WriteLine(ex.Message);
            }
        }

        static public async void NavigateTo(Uri uri)
        {
            try
            {
                await Launcher.LaunchUriAsync(uri);
            }
            catch (Exception ex)
            {
                //AppLogs.WriteError("NavigationServices.NavigateTo", ex);
                Debug.WriteLine(ex.Message);
            }
        }

        public enum NavigationType
        {
            Logout,
            None
        }

    }
}
