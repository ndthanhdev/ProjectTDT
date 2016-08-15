using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.UWP.Views;
using XTDT.API.ServiceAccess;
using XTDT.API.Requests;
using XTDT.API.Respond;
using XTDT.API.Material;
using XTDT.UWP.Services.LocalDataServices;

namespace XTDT.UWP.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public LoginPageViewModel() { }

        bool _isLogging = false;
        string _studentID = string.Empty;
        string _studentPassword = string.Empty;

        public bool IsLogging { get { return _isLogging; } set { Set(ref _isLogging, value); } }
        public string StudentID { get { return _studentID; } set { Set(ref _studentID, value); } }
        public string StudentPassword { get { return _studentPassword; } set { Set(ref _studentPassword, value); } }

        public ICommand LoginCommand => new DelegateCommand(async () => { await Login(); }, () =>
            {
                return !IsLogging;
            });
        private async Task Login()
        {
            try
            {
                Busy.SetBusy(true);
                var package = await Transporter.Transport<AvatarRequest, Avartar>(new AvatarRequest() { user = StudentID, pass = StudentPassword });
                if (package.Status == PackageStatusCode.OK)
                {
                    Busy.SetBusy(true, "Logged");
                    LocalDataService.Instance.StudentID = StudentID;
                    LocalDataService.Instance.Password = StudentPassword;
                    LocalDataService.Instance.Name = package.Respond.Name;
                    LocalDataService.Instance.Avatar = package.Respond.src;
                    LocalDataService.Instance.IsLogged = true;
                    NavigationService.Navigate(typeof(MainPage));
                }

                await Task.Delay(1800);

            }
            catch { }
            finally
            {
                Busy.SetBusy(false);
            }
        }

    }
}
