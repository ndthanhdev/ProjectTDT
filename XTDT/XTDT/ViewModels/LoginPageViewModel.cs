using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Input;

namespace XTDT.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        bool _isLogging = false;
        public bool IsLogging { get { return _isLogging; } set { Set(ref _isLogging, value); } }
        public LoginPageViewModel()
        {
            
        }
        public ICommand LoginCommand => new RelayCommand(
            () =>
            {
                IsLogging = !IsLogging;
            },
            () =>
            {
                Debug.Write(!IsLogging);
                return !IsLogging;
            });
    }
    class test : ICommand
    {
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
