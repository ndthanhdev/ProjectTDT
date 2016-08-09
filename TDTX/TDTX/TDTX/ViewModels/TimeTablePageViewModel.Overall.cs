using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.API;
using TDTX.Common;
using TDTX.Models;
using TDTX.Services;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel
    {
        private ObservableCollection<TimeTableItem> _overallSunday;
        private ObservableCollection<TimeTableItem> _overallMonday;
        private ObservableCollection<TimeTableItem> _overallTuesday;
        private ObservableCollection<TimeTableItem> _overallWednesday;
        private ObservableCollection<TimeTableItem> _overallThursday;
        private ObservableCollection<TimeTableItem> _overallFriday;
        private ObservableCollection<TimeTableItem> _overallSaturday;
        private int _selectedSemesterIndex;

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallSunday
        {
            get { return _overallSunday ?? (_overallSunday = new ObservableCollection<TimeTableItem>()); }
            private set{_overallSunday = value;}
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallMonday
        {
            get { return _overallMonday ?? (_overallMonday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallMonday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallTuesday
        {
            get { return _overallTuesday ?? (_overallTuesday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallTuesday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallWednesday
        {
            get { return _overallWednesday ?? (_overallWednesday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallWednesday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallThursday
        {
            get { return _overallThursday ?? (_overallThursday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallThursday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallFriday
        {
            get { return _overallFriday ?? (_overallFriday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallFriday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<TimeTableItem> OverallSaturday
        {
            get { return _overallSaturday ?? (_overallSaturday = new ObservableCollection<TimeTableItem>()); }
            private set { _overallSaturday = value; }
        }


        /// <summary>
        /// indicated index of SemesterList
        /// </summary>
        public int SelectedSemesterIndex
        {
            get { return _selectedSemesterIndex; }
            set
            {
                _selectedSemesterIndex = value;
                UpdateOverall();
            }
        }

        private async void UpdateOverall()
        {
            await Task.Yield();
            //TODO fix semester changed
            //TODO fix lock
            //TODO add effect
            ClearOverallProperty();

            if (SelectedSemesterIndex < 0 || SelectedSemesterIndex > SemesterDictionary.Count - 1)
                return;
            //add if haven't yet data
            if (SemesterDictionary.Values.ElementAt(SelectedSemesterIndex) == null)
                if (!(await ProvideSemesterData(SelectedSemesterIndex)))
                    return;

            var semester = SemesterDictionary[SemesterDictionary.Keys.ElementAt(SelectedSemesterIndex)];

            if (semester.tkb == null)
                return;

            foreach (var course in semester.tkb)
            {
                foreach (var schedule in course.Lich)
                {
                    TimeTableItem tti = new TimeTableItem() { Course = course, Schedule = schedule };
                    switch (schedule.thu)
                    {
                        case 2:
                            OverallMonday.AddToOrdered(tti);
                            break;
                        case 3:
                            OverallTuesday.AddToOrdered(tti);
                            break;
                        case 4:
                            OverallWednesday.AddToOrdered(tti);
                            break;
                        case 5:
                            OverallThursday.AddToOrdered(tti);
                            break;
                        case 6:
                            OverallFriday.AddToOrdered(tti);
                            break;
                        case 7:
                            OverallSaturday.AddToOrdered(tti);
                            break;
                        default:
                            OverallSunday.AddToOrdered(tti);
                            break;
                    }
                }
            }
        }

        private void ClearOverallProperty()
        {
            OverallMonday.Clear();
            OverallTuesday.Clear();
            OverallWednesday.Clear();
            OverallThursday.Clear();
            OverallFriday.Clear();
            OverallSaturday.Clear();
            OverallSunday.Clear();
        }

    }
}