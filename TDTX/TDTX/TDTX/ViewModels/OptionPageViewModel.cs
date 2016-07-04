using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using GalaSoft.MvvmLight;
using TDTX.Models;

namespace TDTX.ViewModels
{
    class OptionPageViewModel : ViewModelBase
    {
        private static OptionPageViewModel _instance;
        public static OptionPageViewModel Instance => _instance ?? new OptionPageViewModel();
        private ObservableCollection<LanguageItem> _languageItemsitems;
        public ObservableCollection<LanguageItem> LanguageItems
        {
            get { return _languageItemsitems = _languageItemsitems ?? new ObservableCollection<LanguageItem>(); }
            set { Set(ref _languageItemsitems, value); }
        }

        private OptionPageViewModel()
        {
            _languageItemsitems= new ObservableCollection<LanguageItem>()
            {
                new LanguageItem() {CultureName = "en",IconPath = ""},
                new LanguageItem() {CultureName = "lo",IconPath = ""},
                new LanguageItem() {CultureName = "vi",IconPath = ""}
            };

        }
    }
}
