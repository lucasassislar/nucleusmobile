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
            return ((activeConnection != null) && activeConnection.IsConnected);
#elif IOS
            NetworkStatus internetStatus = Reachability.InternetConnectionStatus();
            return internetStatus != NetworkStatus.NotReachable;
#else
            throw new NotImplementedException();
#endif
        }
    }
}