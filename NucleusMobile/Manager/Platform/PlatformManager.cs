using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Nucleus.Droid;
#elif IOS
using Foundation;
using UIKit;
using Nucleus.iOS;
#endif

namespace Nucleus
{
    /// <summary>
    /// Manages platform specific functions
    /// </summary>
    public class PlatformManager : IDisposable
    {
#if ANDROID
        private Activity activity;

        /// <summary>
        /// A reference to the Android activity
        /// </summary>
        public Activity Activity
        {
            get { return activity; }
        }

        /// <summary>
        /// Updates the Android activity
        /// </summary>
        /// <param name="activity"></param>
        public void SetAndroid(Activity activity)
        {
            this.activity = activity;
        }
#elif IOS
        private UIApplicationDelegate del;
        private UIApplication app;
        public UIApplicationDelegate AppDelegate
        {
            get { return del; }
        }

        public UIApplication Application
        {
            get { return app; }
        }
        public void SetIOS(UIApplicationDelegate del, UIApplication app)
        {
            this.del = del;
            this.app = app;
        }
#endif

        /// <summary>
        /// Runs a method on the application's UI thread
        /// </summary>
        /// <param name="a"></param>
        public void RunOnUIThread(Action a)
        {
#if ANDROID
            activity.RunOnUiThread(a);
#elif IOS
            del.InvokeOnMainThread(a);
#endif
        }

        /// <summary>
        /// Finishes the application on supported platforms
        /// </summary>
        public bool EndApplication()
        {
#if ANDROID
            activity.Finish();
            return true;
#elif IOS
            // quitting is not supported on iOS
            return false;
#else
            throw new NotImplementedException();
#endif
        }


        public void Dispose()
        {
        }

        /// <summary>
        /// Copies information from this class over to an instance of this class
        /// </summary>
        /// <param name="platform"></param>
        public void CopyTo(PlatformManager platform)
        {
#if ANDROID
            platform.activity = this.activity;
#elif IOS
            platform.del = this.del;
#endif
        }

        /// <summary>
        /// Gets the Raw screen width of the device (the actual resolution of the device in pixels)
        /// </summary>
        /// <returns></returns>
        public double GetRawScreenWidth()
        {
#if ANDROID
            return AndroidUtil.GetRawViewAreaWidth(activity.Resources);
#elif IOS
            return iOSUtil.GetRawViewWidth();
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Gets the Raw screen height of the device (the actual resolution of the device in pixels)
        /// </summary>
        /// <returns></returns>
        public double GetRawScreenHeight()
        {
#if ANDROID
            return AndroidUtil.GetRawViewAreaHeight(activity.Resources);
#elif IOS
            return iOSUtil.GetRawViewHeight();
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Gets the screen width of the device adjusted for DPI
        /// </summary>
        /// <returns></returns>
        public double GetScreenWidth()
        {
#if ANDROID
            return AndroidUtil.GetViewAreaWidth(activity.Resources);
#elif IOS
            return iOSUtil.GetRawViewWidth();
#else
            throw new NotImplementedException();
#endif
        }

        /// <summary>
        /// Gets the screen height of the device adjusted for DPI
        /// </summary>
        /// <returns></returns>
        public double GetScreenHeight()
        {
#if ANDROID
            return AndroidUtil.GetViewAreaHeight(activity.Resources);
#elif IOS
            return iOSUtil.GetRawViewHeight();
#else
            throw new NotImplementedException();
#endif
        }
    }
}