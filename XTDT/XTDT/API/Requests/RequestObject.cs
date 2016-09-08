using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using XTDT.API.Attributes;

namespace XTDT.API.Requests
{
    public abstract class RequestObject
    {
        public virtual string GenerateQuery()
        {
            Type t = this.GetType();

            var keyValues = from pro in t.GetProperties()
                            where !pro.IsDefined(typeof(APIIgnoreAttribute))
                            select string.Concat(pro.Name, "=", pro.GetValue(this)?.ToString() ?? "");
            return string.Join("&", keyValues);
        }
        /// <summary>
        /// set not null property of other to Instance
        /// </summary>
        /// <param name="other">the Api object use to overwrite this</param>
        public void Overwrite(RequestObject other)
        {
            foreach (var pro in other.GetType().GetProperties())
            {
                if (pro.CanWrite
                    && this.GetType().GetProperty(pro.Name) != null
                    && pro.GetValue(other) != null)
                    pro.SetValue(this, pro.GetValue(other));
            }
        }
    }
}
