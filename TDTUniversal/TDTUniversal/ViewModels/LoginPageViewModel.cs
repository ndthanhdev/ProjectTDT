using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TDTUniversal.API;
using TDTUniversal.API.Requests;
using TDTUniversal.API.Respond;
using TDTUniversal.DataContext;
using TDTUniversal.Services;
using TDTUniversal.Views;
using Template10.Mvvm;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;

namespace TDTUniversal.ViewModels
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

        public ICommand LoginCommand => new DelegateCommand(async () => await Login(), () => !IsLogging);
        
        private async Task Login()
        {
            try
            {
                IsLogging = true;
                var avatar = await ApiClient.GetAsync<AvatarRequest, Avatar>(new AvatarRequest(StudentID, StudentPassword),
                    TokenService.GetTokenProvider());
                if (avatar.Status)
                {
                    //login true
                    LocalDataService.Instance.StudentID = StudentID;
                    LocalDataService.Instance.Password = StudentPassword;
                    LocalDataService.Instance.IsLogged = true;
                    LocalDataService.Instance.Avatar = avatar.Respond.src;
                    LocalDataService.Instance.Name = avatar.Respond.Name;
                    using (var database = new TDTContext())
                    {
                        await database.Database.EnsureCreatedAsync();
                    }
                    await NavigationService.NavigateAsync(typeof(HomePage));
                }
                else
                {
                    //login false
                    var md = new Windows.UI.Popups.MessageDialog("Đăng nhập bất thành");
                    md.Commands.Add(new UICommand("OK") { Id = 0 });
                    await md.ShowAsync();
                }
            }
            catch(Exception ex)
            {
            }
            finally
            {
                IsLogging = false;
            }

        }

    }
}
