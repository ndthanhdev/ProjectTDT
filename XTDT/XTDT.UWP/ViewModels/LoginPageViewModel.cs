using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Template10.Mvvm;
using XTDT.UWP.Views;

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
                await Task.Delay(3000);
            }
            catch { }
            finally
            {
                Busy.SetBusy(false);
            }
        }

    }
}
