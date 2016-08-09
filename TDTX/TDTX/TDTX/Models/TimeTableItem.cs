using System;
using System.Collections.Generic;
using System.Text;
using TDTX.Common;

namespace TDTX.Models
{
    public class TimeTableItem:IComparable<TimeTableItem>
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

        public static int Compare(TimeTableItem x, TimeTableItem y)
        {
            int length = Math.Min(x.Schedule.tiet.Length, y.Schedule.tiet.Length);
            for (int i = 0; i < length; i++)
            {
                if (x.Schedule.tiet[i] != '-' || y.Schedule.tiet[i] != '-')
                {
                    if (x.Schedule.tiet[i] == '-')
                        return 1;
                    if (y.Schedule.tiet[i] == '-')
                        return -1;
                    for (int j = i; j < length; j++)
                    {
                        if (x.Schedule.tiet[i] == '-' || y.Schedule.tiet[i] == '-')
                        {
                            if (x.Schedule.tiet[i] != '-')
                                return 1;
                            if (y.Schedule.tiet[i] != '-')
                                return -1;
                        }
                    }
                    return 0;
                }
            }
            return 0;
        }

        public int CompareTo(TimeTableItem other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return 1;
            }
            return Compare(this, other);
        }
    }
}
