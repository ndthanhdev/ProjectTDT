using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Attributes
{
    /// <summary>
    /// this Attribute use to determine which property is server's require
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class RequestIgnoreAttribute : Attribute
    {
    }
}
