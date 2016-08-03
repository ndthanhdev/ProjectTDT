using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TDTX.Models;
using TDTX.Views;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public class SettingsPageViewModel : ViewModelBase
    {
        private static SettingsPageViewModel _instance;
        public static SettingsPageViewModel Instance => _instance ?? new SettingsPageViewModel();
        private ObservableCollection<LanguageItem> _languageItemsitems;
        public ObservableCollection<LanguageItem> LanguageItems
        {
            get { return _languageItemsitems; }
            set { Set(ref _languageItemsitems, value); }
        }

        private SettingsPageViewModel()
        {
            _languageItemsitems = new ObservableCollection<LanguageItem>()
            {
                new LanguageItem() {CultureName = "en",IconPath = "Images/United_Kingdom_flag_icon.png"},
                new LanguageItem() {CultureName = "lo",IconPath = "Images/Laos_flag_icon.png"},
                new LanguageItem() {CultureName = "vi",IconPath = "Images/Vietnam_flag_icon.png"}
            };
            _instance = this;
        }

        public RelayCommand<string> SelectLanguageCommand => new RelayCommand<string>(
            async ci =>
            {
                await Task.Yield();
                Settings.Instance.Language = ci;
#if UWP
            Windows.UI.Xaml.Application.Current.Exit();
#endif


            });
        public RelayCommand LogoutCommand => new RelayCommand(async () =>
        {
            Application.Current.MainPage = new LoginPage();
            Settings.Instance.UserId = string.Empty;
            Settings.Instance.UserPassword = string.Empty;
            await Task.Yield();
        });
    }
}
