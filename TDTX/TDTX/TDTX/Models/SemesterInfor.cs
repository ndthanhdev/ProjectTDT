using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models
{
    public class SemesterInfor
    {
        public int id { get; set; }

        public string TenHocKy { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is SemesterInfor)
                return id == (obj as SemesterInfor).id;
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return id;
        }
    }
}
