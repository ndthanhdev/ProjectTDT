using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Windows.UI.Core;
using Newtonsoft.Json;
using TDTX.Models;
using TDTX.Models.Base;

namespace TDTX.Services
{
    class Transporter
    {
        public static dynamic Transport(IRequestObject vv)
        {
            return JsonConvert.DeserializeObject(@"{
    ""name"":""Nguyễn Thị Thùy Vân"",
    ""src"":""http:\/\/elearning.tdt.edu.vn\/pluginfile.php\/38501\/user\/icon\/essential\/f1?rev=16829""
}", vv.RespondType);
        }

    }
}
