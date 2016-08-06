using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models
{
    public class Semester
    {
        public DateTime start { get; set; }
        public List<Course> tkb { get; set; }

        public bool IsWorkOnDate(TimeTableItem schedule, DateTime date)
        {
            

            ////go to next sunday if 1 unless add day
            //DateTime firstDay = GetFirstDay(start, M);
            //for (int i = 0; i < schedule.; i++)
            //{
                
            //}
            return false;
        }

        private DateTime GetFirstDay(DateTime start, int dayOfWeek)
        {
            
            return start.AddDays(dayOfWeek < 2 ? 6 : dayOfWeek - 2);
        }
    }
}
