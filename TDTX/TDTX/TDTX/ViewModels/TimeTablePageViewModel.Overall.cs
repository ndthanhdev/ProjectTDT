using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.API;
using TDTX.Models;
using TDTX.Services;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel
    {
        private ObservableCollection<string> _overallSunday;
        private ObservableCollection<string> _overallMonday;
        private ObservableCollection<string> _overallTuesday;
        private ObservableCollection<string> _overallWednesday;
        private ObservableCollection<string> _overallThursday;
        private ObservableCollection<string> _overallFriday;
        private ObservableCollection<string> _overallSaturday;
        private int _selectedSemesterIndex;

        [JsonIgnore]
        public ObservableCollection<string> OverallSunday
        {
            get { return _overallSunday = _overallSunday ?? new ObservableCollection<string>(); }
            private set { _overallSunday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallMonday
        {
            get { return _overallMonday = _overallMonday ?? new ObservableCollection<string>(); }
            private set { _overallMonday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallTuesday
        {
            get { return _overallTuesday = _overallTuesday ?? new ObservableCollection<string>(); }
            private set { _overallTuesday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallWednesday
        {
            get { return _overallWednesday = _overallWednesday ?? new ObservableCollection<string>(); }
            private set { _overallWednesday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallThursday
        {
            get { return _overallThursday = _overallThursday ?? new ObservableCollection<string>(); }
            private set { _overallThursday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallFriday
        {
            get { return _overallFriday = _overallFriday ?? new ObservableCollection<string>(); }
            private set { _overallFriday = value; }
        }

        [JsonIgnore]
        public ObservableCollection<string> OverallSaturday
        {
            get { return _overallSaturday = _overallSaturday ?? new ObservableCollection<string>(); }
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

            ClearOverallProperty();
            // RaiseOverallProperty();

            if (SelectedSemesterIndex < 0 || SelectedSemesterIndex > SemesterDictionary.Count - 1)
                return;
            //add if haven't yet data
            if (SemesterDictionary.Values.ElementAt(SelectedSemesterIndex) == null)
                await ProvideSemesterData(SelectedSemesterIndex);

            var semester = SemesterDictionary[SemesterDictionary.Keys.ElementAt(SelectedSemesterIndex)];


            if (semester.tkb == null)
                return;

            // ClearOverallProperty();
            foreach (var course in semester.tkb)
            {
                foreach (var schedule in course.Lich)
                {
                    switch (schedule.thu)
                    {
                        case "2":
                            OverallMonday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        case "3":
                            OverallTuesday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        case "4":
                            OverallWednesday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        case "5":
                            OverallThursday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        case "6":
                            OverallFriday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        case "7":
                            OverallSaturday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
                            break;
                        default:
                            OverallSunday.Add(course.TenMH + "\n" + schedule.phong + "\n" + schedule.tiet);
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