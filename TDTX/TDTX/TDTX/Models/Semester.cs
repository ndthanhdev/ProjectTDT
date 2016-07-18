using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Models
{
    public class Semester
    {
        public int id { get; set; }

        public string TenHocKy { get; set; }

        public bool CanCompute => true;
    }
}
