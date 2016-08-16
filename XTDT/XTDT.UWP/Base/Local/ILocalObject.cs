using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XTDT.UWP.Base.Local
{
    public interface ILocalObject
    {
        [Newtonsoft.Json.JsonIgnore]
        string Key { get; }
    }
}
