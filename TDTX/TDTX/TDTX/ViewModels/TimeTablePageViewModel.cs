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

        private Page _detail;
        /// <summary>
        /// Current page show on main zone
        /// </summary>
        public Page Detail
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
            Detail = (Page)Activator.CreateInstance(t);
        });

        public delegate void NavigateEventHandler(Page destinationPage);

        public NavigateEventHandler Navigated;
    }
}
