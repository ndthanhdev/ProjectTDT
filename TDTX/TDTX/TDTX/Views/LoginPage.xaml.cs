using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTX.API;
using TDTX.Models;
using TDTX.Services;
using Xamarin.Forms;

namespace TDTX.Views
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();

        }



        private async void Button_OnClicked(object sender, EventArgs e)
        {
            try
            {
                (sender as Button).IsEnabled = false;
                euser.IsEnabled = false;
                epass.IsEnabled = false;
                var respond = await Transporter.Transport<AvatarRequest,Avatar>(
                    new AvatarRequest() { user = euser.Text, pass = epass.Text });
                if (respond.Status == TransportStatusCode.OK)
                {
                    Settings.Instance.UserId = euser.Text;
                    Settings.Instance.UserPassword = epass.Text;
                    Application.Current.MainPage = new MainPage();
                }
                else if (respond.Status == TransportStatusCode.NotAuthorized)
                {
                    await DisplayAlert(Base.TextProvider.Translate("NotAuthorized"),
                        Base.TextProvider.Translate("NotAuthorizedDetail"), "OK");
                }
                else if (respond.Status == TransportStatusCode.Offline)
                {
                    await DisplayAlert(Base.TextProvider.Translate("Offline"),
                        Base.TextProvider.Translate("OfflineDetail"), "OK");
                }
                else
                {
                    await DisplayAlert(Base.TextProvider.Translate("Unknown"),
                        Base.TextProvider.Translate("UnknownDetail"), "OK");
                }
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                (sender as Button).IsEnabled = true;
                euser.IsEnabled = true;
                epass.IsEnabled = true;
            }
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();
            //await Navigation.PopToRootAsync();
        }

        //private override 
    }
}
