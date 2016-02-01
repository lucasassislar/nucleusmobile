#if ANDROID

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Content.PM;
using Android.Content.Res;

namespace Nucleus.Droid
{
    /// <summary>
    /// Android specific Util methods
    /// </summary>
    public static class AndroidUtil
    {
        public static void GetPhotoFromGallery(string message, Activity activity)
        {
            Intent imageIntent = new Intent();
            imageIntent.SetType("image/*");
            imageIntent.SetAction(Intent.ActionGetContent);
            activity.StartActivityForResult(Intent.CreateChooser(imageIntent, message), 0);
        }

        public static double GetViewAreaHeight(Resources res)
        {
            if (res.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                int k = res.GetIdentifier("status_bar_height", "dimen", "android");
                return (res.DisplayMetrics.HeightPixels - res.GetDimensionPixelSize(k)) / res.DisplayMetrics.Density;
            }
            else
            {
                return res.DisplayMetrics.HeightPixels/ res.DisplayMetrics.Density;
            }
        }

        public static double GetViewAreaWidth(Resources res)
        {
            if (res.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                int k = res.GetIdentifier("status_bar_height", "dimen", "android");
                return (res.DisplayMetrics.WidthPixels - res.GetDimensionPixelSize(k)) / res.DisplayMetrics.Density;
            }
            else
            {
                return res.DisplayMetrics.WidthPixels / res.DisplayMetrics.Density;
            }
        }

        public static double GetRawViewAreaWidth(Resources res)
        {
            if (res.Configuration.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                int k = res.GetIdentifier("status_bar_height", "dimen", "android");
                return (res.DisplayMetrics.WidthPixels - res.GetDimensionPixelSize(k));
            }
            else
            {
                return res.DisplayMetrics.WidthPixels;
            }
        }
        public static double GetRawViewAreaHeight(Resources res)
        {
            if (res.Configuration.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                int k = res.GetIdentifier("status_bar_height", "dimen", "android");
                return (res.DisplayMetrics.HeightPixels - res.GetDimensionPixelSize(k));
            }
            else
            {
                return res.DisplayMetrics.HeightPixels;
            }
        }

        public static bool IsThereAnAppToTakePictures(PackageManager pkg)
        {
            Intent intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = pkg.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        public static void MessageBox(Activity activity, string positive, string negative, string title, string message, Action onPositive, Action onNegative)
        {
            new AlertDialog.Builder(activity)
                .SetPositiveButton(positive, (sender, args) =>
                {
                    // User pressed yes
                    if (onPositive != null)
                    {
                        onPositive();
                    }
                })
                .SetNegativeButton(negative, (sender, args) =>
                {
                    // User pressed no 
                    if (onNegative != null)
                    {
                        onNegative();
                    }
                })
                .SetMessage(message)
                .SetTitle(title)
                .Show();
        }

        public static void MessageBox(Activity activity, string positive, string negative, string title, string message)
        {
            MessageBox(activity, positive, negative, title, message, null, null);
        }

        public static void MessageBox(Activity activity, string positive, string title, string message, Action onPositive)
        {
            new AlertDialog.Builder(activity)
                .SetPositiveButton(positive, (sender, args) =>
                {
                    // User pressed yes
                    if (onPositive != null)
                    {
                        onPositive();
                    }
                })
                .SetMessage(message)
                .SetTitle(title)
                .Show();
        }

        public static void MessageBox(Activity activity, string positive, string title, string message)
        {
            MessageBox(activity, positive, title, message, (Action)null);
        }
    }
}
#endif