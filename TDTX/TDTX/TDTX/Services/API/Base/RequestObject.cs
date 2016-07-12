using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace TDTX.Services.API.Base
{
    public abstract class RequestObject
    {
        public string Query 
        {
            get
            {
                var keyValues = from pro in this.GetType().GetProperties()
                                where pro.Name!= "Query"
                                select string.Concat(pro.Name, "=", pro.GetValue(this)?.ToString()??"");
                return "?" + string.Join("&", keyValues);
            }
        }


}
}
