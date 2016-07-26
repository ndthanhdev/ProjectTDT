using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using TDTX.API;
using TDTX.Base;
using TDTX.Models;
using TDTX.Services;
using TDTX.Views;
using TDTX.Views.TimeTableSubs;
using Xamarin.Forms;
using System.Linq;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase, IOnlineContent, ILocalObject
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

        [JsonIgnore]
        public RelayCommand<Type> SelectPageCommand => new RelayCommand<Type>(t =>
        {
            if (t != Detail.GetType())
                Detail = (ContentPage)Activator.CreateInstance(t);
        });

        private Dictionary<SemesterInfor, Semester> _semesterDictionary;
        /// <summary>
        /// provide infor of all semester ready to use
        /// </summary>
        public Dictionary<SemesterInfor, Semester> SemesterDictionary
        {
            get { return _semesterDictionary = _semesterDictionary ?? new Dictionary<SemesterInfor, Semester>(); }
            set
            {
                _semesterDictionary = value;
                MessagingCenter.Send<TimeTablePageViewModel, IDictionary<SemesterInfor, Semester>>(this, "SemesterDictionaryChanged",
                    _semesterDictionary);
            }
        }

        public bool IsNeedUpdate => true;

        public string FileName => "b.json";

        public async Task<bool> UpdateTask()
        {
            await Task.Yield();
            if (!await UpdateListSemester())
                return false;
            for (int i = 0; i < Math.Min(SemesterDictionary.Count, 6); i++)
                await ProvideSemesterData(i);
            return true;
        }

        private async Task<bool> UpdateListSemester()
        {
            var respond = await Transporter.Transport<SemesterListRequest, List<SemesterInfor>>(
               new SemesterListRequest()
               {
                   user = Settings.Instance.UserId,
                   pass = Settings.Instance.UserPassword
               });
            if (respond.Status != TransportStatusCode.OK)
                return false;
            var newDic = new Dictionary<SemesterInfor, Semester>();
            foreach (var si in respond.Respond)
            {
                if (SemesterDictionary.ContainsKey(si))
                    newDic[si] = SemesterDictionary[si];
                else
                    newDic[si] = null;
            }
            SemesterDictionary = newDic;
            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index">index of Dictionary element</param>
        /// <returns></returns>
        private async Task<bool> ProvideSemesterData(int index)
        {
            try
            {
                var respond = await Transporter.Transport<SemesterRequest, Semester>(new SemesterRequest()
                {
                    user = Settings.Instance.UserId,
                    pass = Settings.Instance.UserPassword,
                    id = SemesterDictionary.Keys.ElementAt(index).id
                });
                if (respond.Status == TransportStatusCode.OK)
                    SemesterDictionary[SemesterDictionary.Keys.ElementAt(index)] = respond.Respond;
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
