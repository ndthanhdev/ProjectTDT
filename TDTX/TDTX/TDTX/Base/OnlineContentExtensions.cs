using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TDTX.Base
{
    public static class OnlineContentExtensions
    {
        public static async Task<bool> UpdateAsync(this IOnlineContent content)
        {
            await Task.Yield();
            try
            {
                if (content.IsNeedUpdate)
                    return await content.UpdateTask();
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
