using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectTDTWindows.Model
{
    public class SemesterInforModel
    {
        public int BeginWeek { set; get; }
        public DateTime BeginCourse { set; get; }
        public string Name { set; get; }
        public DateTime EndCourse
        {
            get
            { return BeginCourse.AddDays(363); }
        }
    }
}
