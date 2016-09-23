using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TDTUniversal.DataContext;

namespace TDTUniversal.Models
{
    public class MonHocLichHoc
    {
        public MonHocLichHoc(MonHoc monHoc, LichHoc lichHoc)
        {
            MonHoc = monHoc;
            LichHoc = lichHoc;
        }
        public MonHoc MonHoc { get; set; }
        public LichHoc LichHoc { get; set; }
    }
}
