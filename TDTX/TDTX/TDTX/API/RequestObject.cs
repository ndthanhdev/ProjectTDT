using System;
using System.Linq;
using System.Reflection;


namespace TDTX.API
{
    public abstract class RequestObject
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
        public void Overwrite(RequestObject other)
        {
            foreach (var pro in other.GetType().GetProperties())
            {
                if(pro.CanWrite
                    &&this.GetType().GetProperty(pro.Name)!=null
                    &&pro.GetValue(other)!=null)
                    pro.SetValue(this,pro.GetValue(other));
            }
        }
    }
}
