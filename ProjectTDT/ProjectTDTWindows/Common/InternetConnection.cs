using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;
using Windows.Networking.Connectivity;
using System.Linq;


namespace ProjectTDTWindows.Common
{
    public static class InternetConnection
    {
        public static bool IsInternetAvailable()
        {
            if (!NetworkInterface.GetIsNetworkAvailable())
            {
                return false;
            }

            return NetworkInformation.GetConnectionProfiles().Any(p=>IsInternetProfile(p));
        }

        private static bool IsInternetProfile(ConnectionProfile connectionProfile)
        {
            if (connectionProfile == null)
            {
                return false;
            }

            var connectivityLevel = connectionProfile.GetNetworkConnectivityLevel();
            return connectivityLevel != NetworkConnectivityLevel.None;
        }
    }
}
