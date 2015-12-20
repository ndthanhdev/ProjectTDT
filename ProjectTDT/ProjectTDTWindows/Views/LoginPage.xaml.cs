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
using ProjectTDTWindows.Services;
using ProjectTDTWindows.Model;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectTDTWindows.Views
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
            Windows.Security.Credentials.PasswordCredential credential=  CredentialServices.GetCredentialFromLocker();
            tbxID.Text = credential.UserName;
            tbxPassword.Password = credential.Password;          
           
        }

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            //start Login, disable btnLogin first
            btnLogin.IsEnabled = false;
            tbxID.IsEnabled = false;
            tbxPassword.IsEnabled = false;
            PgRing.IsActive = true;

            
            CredentialServices.SetCredential(tbxID.Text==""?"TDTU":tbxID.Text, tbxPassword.Password == "" ? "www" : tbxPassword.Password);
            TDTClient cl = new TDTClient(tbxID.Text, tbxPassword.Password);
            if ((await cl.tryLogin()) == true)
            {
                SettingServices.NameOfUser = cl.Name;
                SettingServices.IsLogged=true;
                App.RootFrame.Navigate(typeof(MainPage));
            }
            else
            {
                SettingServices.IsLogged = false;
            }

            //End Login 
            btnLogin.IsEnabled = true;
            tbxID.IsEnabled = true;
            tbxPassword.IsEnabled = true;
            PgRing.IsActive = false;


        }

       
    }
}
