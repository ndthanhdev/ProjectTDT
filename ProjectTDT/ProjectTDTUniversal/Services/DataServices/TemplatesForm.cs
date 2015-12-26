using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDTUniversal.Services.DataServices
{
    public static class TemplatesForm
    {
        public readonly static HttpForm Login = new HttpForm(new Uri("https://student.tdt.edu.vn/taikhoan/dangnhap"), "TextMSSV", "PassMK");

        public readonly static HttpForm GetNotify = new HttpForm(new Uri("https://student.tdt.edu.vn/tb"));
    }

}
