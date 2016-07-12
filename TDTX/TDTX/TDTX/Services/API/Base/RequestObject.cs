using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace TDTX.Services.API.Base
{
    public class RequestObject
    {
        public string Query 
        {
            get
            {
                var keyValues = from pro in this.GetType().GetProperties()
                                select string.Concat(pro.Name, "=", pro.GetValue(this).ToString());
                return "?" + string.Join("&", keyValues);
            }
        }


}
}
