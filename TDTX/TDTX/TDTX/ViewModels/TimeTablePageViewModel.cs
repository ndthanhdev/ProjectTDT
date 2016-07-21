using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TDTX.Views;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public class TimeTablePageViewModel : ViewModelBase
    {
        private static TimeTablePageViewModel _instance;
        public static TimeTablePageViewModel Instance => _instance ?? new TimeTablePageViewModel();

        private TimeTablePageViewModel()
        {
            DetailPage = new SettingsPage();
            _instance = this;
        }

        public Page DetailPage { get; set; }
        public RelayCommand<Type> SelectPageCommand => new RelayCommand<Type>(t =>
        {
            Navigated((Page)Activator.CreateInstance(t));
        });

        public delegate void NavigateEventHandler(Page destinationPage);

        public NavigateEventHandler Navigated;
    }
}
