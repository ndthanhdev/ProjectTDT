using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models
{
    public class CourseSchedule
    {
        public string tiet { get; set; }
        public int thu { get; set; }
        public string phong { get; set; }
        public string tuan { get; set; }

        public static DayOfWeek DayOfWeekFromThu(int thu)
        {

            return (DayOfWeek)thu - 1;
        }
    }
}
