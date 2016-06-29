using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TDTX.Views.Extensions.Base
{
    public static class Ext
    {
        public static PropertyInfo GetPropertyEx(this Type type, string name)
        {
            while (type != null)
            {
                var typeInfo = type.GetTypeInfo();
                var p = typeInfo.GetDeclaredProperty(name);
                if (p != null)
                    return p;
                type = typeInfo.BaseType;
            }
            return null;
        }
    }
}
