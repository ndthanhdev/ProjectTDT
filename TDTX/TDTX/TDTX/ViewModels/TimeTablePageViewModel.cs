using System;
using System.Collections.Generic;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using TDTX.Models;
using TDTX.Views;
using TDTX.Views.TimeTableSubs;
using Xamarin.Forms;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        private static TimeTablePageViewModel _instance;
        public static TimeTablePageViewModel Instance => _instance ?? new TimeTablePageViewModel();

        private ContentPage _detail;
        /// <summary>
        /// Current page show on main zone
        /// </summary>
        [JsonIgnore]
        public ContentPage Detail
        {
            get { return _detail = _detail ?? new DayPage(); }
            set
            {
                _detail = value;
                MessagingCenter.Send<TimeTablePageViewModel, ContentPage>(this, "Navigated", _detail);
            }
        }

        private TimeTablePageViewModel()
        {
            _instance = this;
        }

        public RelayCommand<Type> SelectPageCommand => new RelayCommand<Type>(t =>
        {
            if (t != Detail.GetType())
                Detail = (ContentPage)Activator.CreateInstance(t);
        });

        private List<SemesterInfor> _semesterInforList;

        public List<SemesterInfor> SemesterInforList
        {
            get
            {
                return _semesterInforList = _semesterInforList ?? new List<SemesterInfor>()
            {
                new SemesterInfor() {id = 83,TenHocKy = "HK 3/HK hè 2015-2016"}
            };
            }
            set
            {
                _semesterInforList = value;
                MessagingCenter.Send<TimeTablePageViewModel, IList<SemesterInfor>>(this, "SemesterListChanged", _semesterInforList);
            }
        }

        private Dictionary<SemesterInfor, Semester> _semesterDictionary;
        /// <summary>
        /// provide infor of all semester ready to use
        /// </summary>
        public Dictionary<SemesterInfor, Semester> SemesterDictionary
        {
            get { return _semesterDictionary = _semesterDictionary ?? new Dictionary<SemesterInfor, Semester>(); }
            set { _semesterDictionary = value; }
        }
    }
}
