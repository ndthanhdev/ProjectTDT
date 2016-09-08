using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = true)]
    public class RequestParameterAttribute : Attribute
    {
        public string Name;
        public RequestParameterAttribute()
        {
        }
    }
}
