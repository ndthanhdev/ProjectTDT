using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace TDTX.Base.API
{
    public abstract class ApiObject
    {
        public string Query
        {
            get
            {
                Type t = this.GetType();
                
                var keyValues = from pro in t.GetProperties()
                                where pro.Name != "Query"
                                && pro.IsDefined(typeof(RequestPropertyAttribute))
                                select string.Concat(pro.Name, "=", pro.GetValue(this)?.ToString() ?? "");
                return string.Join("&", keyValues);
            }
        }
    }
}
