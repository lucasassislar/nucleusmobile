using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if ANDROID
using Xamarin.Forms.Platform.Android;
using Android.Widget;
#elif IOS
using Xamarin.Forms.Platform.iOS;
using UIKit;
#endif


namespace Nucleus
{
    public class CustomImageRenderer : ImageRenderer
    {
#if ANDROID
        protected override bool DrawChild(Android.Graphics.Canvas canvas, Android.Views.View child, long drawingTime)
        {
            if (child is ImageView)
            {
                CustomImage custom = (CustomImage)this.Element;
                custom.ImgView = (ImageView)child;
            }

            return base.DrawChild(canvas, child, drawingTime);
        }
#elif IOS
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Image> e)
        {
            base.OnElementChanged(e);
            if (Element == null)
            {
                return;
            }

            if (Control == null)
            {
                SetNativeControl(new UIImageView()
                {
                    ContentMode = UIViewContentMode.ScaleAspectFit,
                    ClipsToBounds = true
                });
            }


            UIImageView view = Control;
            CustomImage img = (CustomImage)Element;
            img.ImageView = view;
        }
#endif
    }
}