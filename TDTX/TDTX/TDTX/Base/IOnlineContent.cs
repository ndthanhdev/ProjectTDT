using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TDTX.Base
{
    public interface IOnlineContent
    {
        bool IsNeedUpdate { get; }
        Task<bool> UpdateTask();
    }
}
