using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

#if ANDROID
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Graphics;
#elif IOS
using UIKit;
#endif

namespace Nucleus
{
    /// <summary>
    /// A Resource holding an Image. Loads the image in a platform specific manner, can be disposed by the
    /// OS with no warning (always check for IsDisposed() before using this)
    /// </summary>
    public class ImageResource : IResourceObject
    {
        private MemoryStream imageStream;
#if ANDROID
        private Bitmap bitmap;
        public Bitmap Bitmap
        {
            get { return bitmap; }
        }
#elif IOS
        private UIImage image;
        public UIImage Image
        {
            get { return image; }
        }
#endif

        public MemoryStream ImageStream
        {
            get { return imageStream; }
        }

        public ImageResource(byte[] daa)
        {
            imageStream = new MemoryStream(daa);
        }
#if ANDROID
        public ImageResource(Bitmap bitmap)
        {
            this.bitmap = bitmap;
        }
#elif IOS
        public ImageResource(UIImage image)
        {
            this.image = image;
        }
#endif

        public void Dispose()
        {
            if (imageStream != null)
            {
                imageStream.Dispose();
                imageStream = null;
            }

#if ANDROID
            if (bitmap != null)
            {
                bitmap.Dispose();
                bitmap = null;
            }
#elif IOS
            if (image != null)
            {
                image.Dispose();
                image = null;
            }
#endif
        }

        public bool IsDisposed()
        {
#if ANDROID
            try
            {
                return bitmap.IsRecycled;
            }
            catch
            {
                return true;
            }
#elif IOS
            try
            {
                return image.Self == null; // not tested! TODO
            }
            catch { return true; }
#else
            throw new NotImplementedException();
#endif
        }
    }
}