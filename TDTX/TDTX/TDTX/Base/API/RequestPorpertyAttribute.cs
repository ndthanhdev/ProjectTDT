using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX.Base
{
    [AttributeUsage(AttributeTargets.Property,Inherited = true,AllowMultiple = true)]
    public class RequestPropertyAttribute:Attribute
    {
    }
}
