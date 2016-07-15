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
        /// <summary>
        /// set not null property of other to Instance
        /// </summary>
        /// <param name="other">the Api object use to overwrite this</param>
        public void Overwrite(ApiObject other)
        {
            foreach (var pro in other.GetType().GetProperties())
            {
                if(this.GetType().GetProperty(pro.Name)!=null&&pro.GetValue(this)!=null)
                    pro.SetValue(this,pro.GetValue(other));
            }
        }
    }
}
