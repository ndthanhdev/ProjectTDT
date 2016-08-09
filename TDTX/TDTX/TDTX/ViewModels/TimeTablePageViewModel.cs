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
using Newtonsoft.Json.Serialization;
using TDTX.Common;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase, IOnlineContent
    {
        private static TimeTablePageViewModel _instance;
        public static TimeTablePageViewModel Instance => _instance ?? new TimeTablePageViewModel();

        private Page _detail;

        /// <summary>
        /// Current page show on main zone
        /// </summary>
        [JsonIgnore]
        public Page Detail
        {
            get { return _detail = _detail ?? new OverallPage(); }
            set
            {
                _detail = value;
                MessagingCenter.Send<TimeTablePageViewModel, Page>(this, "Navigated", _detail);
            }
        }


        private TimeTablePageViewModel()
        {
            //_detail = new OverallPage();

            //Day = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();

            //OverallSunday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallMonday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallTuesday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallWednesday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallThursday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallFriday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallSaturday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();
            //OverallSunday = new System.Collections.ObjectModel.ObservableCollection<TimeTableItem>();

            _instance = this;
        }

        [JsonIgnore]
        public RelayCommand<Type> SelectPageCommand => new RelayCommand<Type>(t =>
        {
            if (t != Detail.GetType())
                Detail = (Page)Activator.CreateInstance(t);
        });


        /// <summary>
        /// provide infor of all semester ready to use
        /// </summary>
        public DictionarySerializeToArray<SemesterInfor, Semester> SemesterDictionary
        {
            get { return TimeTable.Instance.SemesterDictionary; }
            set
            {
                TimeTable.Instance.SemesterDictionary = value;
                MessagingCenter.Send<TimeTablePageViewModel, IDictionary<SemesterInfor, Semester>>(this,
                    "SemesterDictionaryChanged",
                    TimeTable.Instance.SemesterDictionary);
            }
        }

        public bool IsNeedUpdate => true;

        public async Task<bool> UpdateTask()
        {
            await Task.Yield();
            if (!await UpdateListSemester())
                return false;
            //TODO try multi requesta
            List<Task> provideSemesters = new List<Task>();
            for (int i = 0; i < Math.Min(SemesterDictionary.Count, 6); i++)
                provideSemesters.Add(ProvideSemesterData(i));
            await Task.WhenAll(provideSemesters);
            await Task.WhenAll(new Task(UpdateDay), new Task(UpdateOverall));
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
            var newDic = new DictionarySerializeToArray<SemesterInfor, Semester>();
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
                {
                    SemesterDictionary[SemesterDictionary.Keys.ElementAt(index)] = respond.Respond;
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}