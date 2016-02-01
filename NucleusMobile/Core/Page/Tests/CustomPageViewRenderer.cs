using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#if ANDROID
using Xamarin.Forms.Platform.Android;
#elif IOS
using Xamarin.Forms.Platform.iOS;
#endif

namespace Nucleus
{
    public class CustomPageViewRenderer : ViewRenderer
    {

#if ANDROID
        public override bool OnTouchEvent(Android.Views.MotionEvent e)
        {
            if (this.Element is CustomPageView)
            {
                var view = this.Control;
                TouchData data = TouchData.FromAndroid(e);
                CustomPageView custom = (CustomPageView)this.Element;
                custom.OnTouch(data);
            }

            return true;
        }
#elif IOS

#endif
    }
}