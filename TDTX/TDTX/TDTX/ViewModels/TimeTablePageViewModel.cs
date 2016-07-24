using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using TDTX.Views;
using TDTX.Views.TimeTableSubs;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public class TimeTablePageViewModel : ViewModelBase
    {
        private static TimeTablePageViewModel _instance;
        public static TimeTablePageViewModel Instance => _instance ?? new TimeTablePageViewModel();

        private ContentPage _detail;
        /// <summary>
        /// Current page show on main zone
        /// </summary>
        public ContentPage Detail
        {
            get { return _detail = _detail ?? new DayPage(); }
            set
            {
                _detail = value;

                Navigated?.Invoke(_detail);
            }
        }

        private TimeTablePageViewModel()
        {
            _instance = this;
        }

        public RelayCommand<Type> SelectPageCommand => new RelayCommand<Type>(t =>
        {
            Detail = (ContentPage)Activator.CreateInstance(t);
        });

        public delegate void NavigateEventHandler(ContentPage destinationPage);

        public NavigateEventHandler Navigated;
    }
}
