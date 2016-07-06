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
    class OptionPageViewModel : ViewModelBase
    {
        private static OptionPageViewModel _instance;
        public static OptionPageViewModel Instance => _instance ?? new OptionPageViewModel();
        private ObservableCollection<LanguageItem> _languageItemsitems;
        public ObservableCollection<LanguageItem> LanguageItems
        {
            get { return _languageItemsitems; }
            set { Set(ref _languageItemsitems, value); }
        }

        private OptionPageViewModel()
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
                Application.Current.Properties[nameof(App.AppProperties.Dictionary)] = new CultureInfo(ci);
                ((MasterDetailPage)Application.Current.MainPage).Detail =
                    new NavigationPage((Page)Activator.CreateInstance(typeof(OptionPage)));
                await Task.Yield();
            });
    }
}
