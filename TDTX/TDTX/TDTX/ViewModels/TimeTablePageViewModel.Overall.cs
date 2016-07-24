using System;
using System.Collections.Generic;
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
        /// <summary>
        /// indicated index of SemesterList
        /// </summary>
        [JsonIgnore]
        public int SelectedSemesterIndex { get; set; }

        public async void UpdateOverall()
        {
            await Task.Yield();
            if (SelectedSemesterIndex < 0 || SelectedSemesterIndex > SemesterInforList.Count - 1)
                return;
            //add if not exist
            if (!SemesterDictionary.ContainsKey(SemesterInforList[SelectedSemesterIndex]))
            {
                var respond = await Transporter.Transport<SemesterRequest, Semester>(new SemesterRequest()
                {
                    user = Settings.Instance.UserId,
                    pass = Settings.Instance.UserPassword
                });
                if (respond.Status == TransportStatusCode.OK)
                    SemesterDictionary.Add(SemesterInforList[SelectedSemesterIndex], respond.Respond);
            }
            var semester = SemesterDictionary[SemesterInforList[SelectedSemesterIndex]];

            OverallMonday = new List<string>();
            OverallTuesday = new List<string>();
            OverallWednesday = new List<string>();
            OverallThursday = new List<string>();
            OverallFriday = new List<string>();
            OverallSaturday = new List<string>();
            OverallSunday = new List<string>();

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
            RaisePropertyChanged(nameof(OverallMonday));
            RaisePropertyChanged(nameof(OverallTuesday));
            RaisePropertyChanged(nameof(OverallWednesday));
            RaisePropertyChanged(nameof(OverallThursday));
            RaisePropertyChanged(nameof(OverallFriday));
            RaisePropertyChanged(nameof(OverallSaturday));
            RaisePropertyChanged(nameof(OverallSunday));

        }

        public List<string> OverallMonday { get; private set; }
        public List<string> OverallTuesday { get; private set; }
        public List<string> OverallWednesday { get; private set; }
        public List<string> OverallThursday { get; private set; }
        public List<string> OverallFriday { get; private set; }
        public List<string> OverallSaturday { get; private set; }
        public List<string> OverallSunday { get; private set; }

    }
}
