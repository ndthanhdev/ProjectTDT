using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;
using XTDT.UWP.Common;

namespace XTDT.UWP.ViewModels
{
    public partial class TimeTablePageViewModel : ViewModelBase
    {
        private ScheduleAppointmentCollection _schedule;
        public ScheduleAppointmentCollection Schedule
        {
            get { return _schedule ?? (_schedule = new ScheduleAppointmentCollection()); }
            set { Set(ref _schedule, value); }
        }

        public async Task InitializeCalendar()
        {
            await Task.Yield();
            Schedule.Clear();
            foreach (var hk in DataCotroller.HocKyDictionary.Values)
            {
                if (hk == null || hk.Tkb == null)
                    continue;
                DateTime root = hk.Start;
                foreach (var tkb in hk.Tkb)
                {
                    foreach (var lich in tkb.Lich)
                    {
                        TimeSpan dayOfWeek = ThuToBonusDay(lich.Thu);
                        DateTime rootDate = root + dayOfWeek;
                        DateTime rootStart = rootDate + Ultility.GetBeginTime(lich.Tiet);
                        DateTime rootEnd = rootDate + Ultility.GetEndTime(lich.Tiet);
                        for (int i = 0; i < lich.Tuan.Length; i++)
                        {
                            if (lich.Tuan[i] != '-')
                            {
                                ScheduleAppointment sa = new ScheduleAppointment();
                                sa.StartTime = rootStart + TimeSpan.FromDays(i * 7);
                                sa.EndTime = rootEnd + TimeSpan.FromDays(i * 7);
                                sa.Subject = string.Format("{0} ({1})", tkb.TenMH, lich.Phong);
                                sa.Notes = string.Format("Mã môn học:{0}\nNhóm:{1}\nTổ:{2}\nThứ:{3}\nTuần:{4}",
                                    tkb.MaMH, tkb.Nhom, tkb.To, lich.Thu, lich.Tuan);
                                Schedule.Add(sa);
                            }
                        }
                    }
                }
            }
        }

    }
}
