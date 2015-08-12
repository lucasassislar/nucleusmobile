using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if ANDROID
using Android.App;
using Android.Net;
#elif IOS
using Nucleus.iOS;
#endif

namespace Nucleus
{
    public static class NetworkUtil
    {
        public static bool IsConnected()
        {
#if ANDROID
            var activity = Core.Instance.PlatformManager.Activity;
            var connectivityManager = (ConnectivityManager)activity.GetSystemService(Activity.ConnectivityService);
            var activeConnection = connectivityManager.ActiveNetworkInfo;
            if ((activeConnection != null) && activeConnection.IsConnected)
            {
                // we are connected to a network
                return true;
            }
#elif IOS
            NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
            return internetStatus != NetworkStatus.NotReachable;
#else
            throw new NotImplementedException();
#endif
        }
    }
}