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
        private DateTime _selectedDateTime = DateTime.Parse("08/15/2016");

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
                UpdateDay();
            }
        }

        public async void UpdateDay()
        {
            await Task.Yield();
            ObservableCollection<TimeTableItem> tempList = new ObservableCollection<TimeTableItem>();
            foreach (KeyValuePair<SemesterInfor, Semester> keyValuePair in SemesterDictionary)
            {
                if (keyValuePair.Value == null)
                    continue;
                if (keyValuePair.Value.tkb == null)
                    continue;
                if (keyValuePair.Value.start > SelectedDateTime)
                    continue;
                if (SelectedDateTime.Subtract(keyValuePair.Value.start).Days > 365)
                    break;
                foreach (var course in keyValuePair.Value.tkb)
                {
                    foreach (var courseSchedule in course.Lich)
                    {
                       if(IsWorkOnDate(courseSchedule,keyValuePair.Value.start,SelectedDateTime))
                            tempList.Add(new TimeTableItem()
                            {
                                Course = course,
                                Schedule = courseSchedule
                            });
                    }
                }
            }
            lock (Day)
            {
                Day.Clear();
                foreach (var timeTableItem in tempList)
                {
                    Day.Add(timeTableItem);
                }
            }
        }

        private bool IsWorkOnDate(CourseSchedule schedule, DateTime startDate, DateTime date)
        {
            if (date < startDate)
                return false;
            int differenceDay = date.Subtract(startDate).Days;
            int index = differenceDay / 7;
            if (index > schedule.tuan.Length - 1)
                return false;
            if (schedule.tuan[index] == '-')
                return false;
            return DayInWeek(startDate.AddDays(index * 7), schedule.thu) == date;
        }
        /// <summary>
        /// using startDate of week and dayOfWeek to calculate real date
        /// </summary>
        /// <param name="start"></param>
        /// <param name="dayOfWeek"></param>
        /// <returns></returns>
        private DateTime DayInWeek(DateTime start, int dayOfWeek)
        {

            return start.AddDays(dayOfWeek < 2 ? 6 : dayOfWeek - 2);
        }


    }
}

