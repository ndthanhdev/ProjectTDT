using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TDTX.Dependencies;
using Xamarin.Forms;

namespace TDTX.Base
{
    public interface ILocalObject
    {
        [JsonIgnore]
        string FileName { get; }

    }
}
