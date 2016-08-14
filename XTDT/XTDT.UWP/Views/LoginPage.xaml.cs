using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using XTDT.UWP.Services.LocalDataServices;
using XTDT.UWP.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace XTDT.UWP.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            LocalDataService.Instance.IsLogged = false;
            //foreach (var hamburgerButton in Shell.HamburgerMenu.PrimaryButtons)
            //    hamburgerButton.IsEnabled = false;
            //LoginButton.Content = "Login";
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            bool state = LocalDataService.Instance.IsLogged;
            //foreach (var hamburgerButton in Shell.HamburgerMenu.PrimaryButtons)
            //    hamburgerButton.IsEnabled = state;
            //if (state)
            //    LoginButton.Content = "Logout";
        }
    }
}
