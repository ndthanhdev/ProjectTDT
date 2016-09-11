using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDTUniversal.API
{
    public class Package<T, U>
    {
        public T Request { get; set; }
        public U Respond { get; set; }
        public bool Status { get; set; }
    }
}
