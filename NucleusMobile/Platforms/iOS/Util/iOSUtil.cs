#if IOS

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using UIKit;

namespace Nucleus.iOS
{
    public static class iOSUtil
    {
        public static double GetRawViewWidth()
        {
            return UIScreen.MainScreen.Bounds.Width;
        }

        public static double GetRawViewHeight()
        {
            return UIScreen.MainScreen.Bounds.Height;
        }

        public static double GetViewWidth()
        {
            double width = UIScreen.MainScreen.Bounds.Width;

            return width;
        }
        public static double GetViewHeight()
        {
            double height = UIScreen.MainScreen.Bounds.Height;
            height -= (20 + 44);
            return height;
        }
    }
}
#endif