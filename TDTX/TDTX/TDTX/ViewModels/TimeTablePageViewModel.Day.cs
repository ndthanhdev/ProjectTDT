using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using TDTX.Models;

namespace TDTX.ViewModels
{
    public partial class TimeTablePageViewModel
    {
        private ObservableCollection<TimeTableItem> _day;
        private DateTime _selectedDateTime;

        public ObservableCollection<TimeTableItem> Day
        {
            get { return _day = _day ?? new ObservableCollection<TimeTableItem>(); }
        }

        public DateTime SelectedDateTime
        {
            get { return _selectedDateTime; }
            set
            {
                _selectedDateTime = value;
                UpdateTask().RunSynchronously();
            }
        }

        public async void UpdateDay()
        {
            await Task.Yield();
            foreach (KeyValuePair<SemesterInfor, Semester> keyValuePair in SemesterDictionary)
            {
                if (keyValuePair.Value == null)
                    continue;
                if (keyValuePair.Value.start > DateTime.Now)
                    continue;
                if (DateTime.Now.Subtract(keyValuePair.Value.start).Days > 365)
                    break;
                //SemesterDictionary.co

            }
        }


    }
}
