using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TDTX.Base
{
    public interface IOnlineContent
    {
        [JsonIgnore]
        bool IsNeedUpdate { get; }

        Task<bool> UpdateTask();
    }
}
