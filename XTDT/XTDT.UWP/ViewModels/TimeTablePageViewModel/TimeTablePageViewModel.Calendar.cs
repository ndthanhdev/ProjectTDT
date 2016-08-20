using Syncfusion.UI.Xaml.Schedule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Template10.Mvvm;

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

        }

    }
}
