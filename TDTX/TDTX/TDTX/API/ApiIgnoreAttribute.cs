using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.API
{
    /// <summary>
    /// this Attribute use to determine which property is server's require
    /// </summary>
    [AttributeUsage(AttributeTargets.Property,Inherited = true,AllowMultiple = true)]
    public class ApiIgnoreAttribute:Attribute
    {
        //TODO improve name in derived class
    }
}
