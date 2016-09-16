using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using TDTUniversal.DataContext;
using TDTUniversal.Services;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace TDTUniversal.Views
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
            foreach (var hamburgerButton in Shell.HamburgerMenu.PrimaryButtons)
                hamburgerButton.IsEnabled = false;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)

        {

            base.OnNavigatedFrom(e);
            bool state = LocalDataService.Instance.IsLogged;
            foreach (var hamburgerButton in Shell.HamburgerMenu.PrimaryButtons)
                hamburgerButton.IsEnabled = state;
        }

    }
}
