using ProjectTDTUniversal.Services.DataServices;
using ProjectTDTUniversal.Services.SettingsServices;
using ProjectTDTUniversal.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Navigation;
using Windows.Foundation;
using Windows.Storage.Streams;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;


using System.Threading;
using Windows.Web.Http.Filters;
using Windows.Web.Http;
using Windows.Web.Http.Headers;

namespace ProjectTDTUniversal.ViewModels
{
    public class LogInPageViewModel:Mvvm.ViewModelBase
    {
        public LogInPageViewModel()
        {

        }

        string _mssv;
        public string MSSV { get { return CredentialsService.GetCredential().UserName; } set { Set(ref _mssv, value); } }

        string _mk;
        public string MK { get { return CredentialsService.GetCredential().Password; } set { Set(ref _mk, value); } }

        public override void OnNavigatedTo(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            SettingsService.Instance.LoginStatus = false;
            if (state.ContainsKey(nameof(MSSV)))
                MSSV = state[nameof(MSSV)]?.ToString();

            if (state.ContainsKey(nameof(MK)))
                MK = state[nameof(MK)]?.ToString();  
            state.Clear();
            
        }

        public override async Task OnNavigatedFromAsync(IDictionary<string, object> state, bool suspending)
        {
            if (suspending)
            {
                state[nameof(MSSV)] = MSSV;
                state[nameof(MK)] = MK;
            }            
            await Task.Yield();
        }

        public void GotoPrivacy()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 1);
        }
        public void GotoAbout()
        {
            NavigationService.Navigate(typeof(Views.SettingsPage), 2);
        }


        public async void LogIn()
        {
            try
            {
                Shell.SetBusy(true, "Đang đăng nhập...");
                if (!CredentialsService.SetCredential(MSSV, MK))
                    return;
                SettingsService.Instance.LoginStatus = await Transporter.Instance.Login();
                if (SettingsService.Instance.LoginStatus)
                    NavigationService.Navigate(typeof(NotifyPage));
            }
            catch(Exception ex)
            {
                throw ex;
            }
            finally
            {                
                    Shell.SetBusy(false);
            }           
        }

    }
}
