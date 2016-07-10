using System;
using System.Collections.Generic;
using System.Text;

namespace TDTX
{
    public static class AppExtensions
    {
        /// <summary>
        /// check if UserID and Password is not null
        /// to navigate to MainPage
        /// </summary>
        /// <param name="settings"></param>
        /// <returns></returns>
        public static bool CanTryLogin(this Settings settings)
        {
            return !( String.IsNullOrEmpty(settings.UserId) && String.IsNullOrEmpty(settings.UserPassword));
        }
    }
}
