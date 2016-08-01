using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Views.Base;

namespace TDTX.Models
{
    public class TimeTableItem
    {
        public Course Course { get; set; }
        public CourseSchedule Schedule { get; set; }

        public string ProvideDetail()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0} : {1}\n", TextProvider.Translate("SubjectCode"), Course.MaMH);
            sb.AppendFormat("{0} : {1}\n", TextProvider.Translate("Group"), Course.Nhom ?? "");
            sb.AppendFormat("{0} : {1}\n", TextProvider.Translate("Team"), Course.To ?? "");
            sb.AppendFormat("{0} : {1}\n", TextProvider.Translate("Week"), Schedule.tuan);
            return sb.ToString();
        }
    }
}
