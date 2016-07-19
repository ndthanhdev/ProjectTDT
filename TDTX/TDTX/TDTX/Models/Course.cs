using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models
{
    public class Course
    {
        public string MaMH { get; set; }
        public string TenMH { get; set; }
        public string Nhom { get; set; }
        public string To { get; set; }
        public CourseSchedule[] Lich { get; set; }
    }
}
