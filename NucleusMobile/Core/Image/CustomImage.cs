using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
#if ANDROID
using Android.Widget;
using Android.Graphics;
#elif IOS
using UIKit;
#endif

namespace Nucleus
{
    public class CustomImage : Image
    {
        public ImageResource Resource;

#if ANDROID
        private ImageView imgView;
        private Bitmap bitmap;

        public ImageView ImgView
        {
            get { return imgView; }
            set
            {
                imgView = value;
                UpdateImgView();
            }
        }
        public Bitmap Bitmap
        {
            get { return bitmap; }
            set
            {
                bitmap = value;
                UpdateImgView();
            }
        }

#elif IOS
        private UIImageView imgView;
        private UIImage image;

        public UIImageView ImageView
        {
            get { return imgView; }
            set
            {
                imgView = value;
                UpdateImgView();
            }
        }

        public UIImage Image
        {
            get { return image; }
            set
            {
                image = value;
                UpdateImgView();
            }
        }

#endif

        private string source;
        public string NSource
        {
            get { return source; }
            set
            {
                source = value;
                Core.Instance.ResourceManager.LoadFile(value, ResourceType.Image, ReceiveImage, null, true);
            }
        }


        private void UpdateImgView()
        {
#if ANDROID
            if (imgView != null && Bitmap != null && !Resource.IsDisposed())
            {
                Core.Instance.PlatformManager.RunOnUIThread(delegate
                {
                    imgView.SetImageBitmap(Bitmap);
                });
            }
#elif IOS
            if (imgView != null && image != null && !Resource.IsDisposed())
            {
                Core.Instance.PlatformManager.RunOnUIThread(delegate
                {
                    imgView.Image = image;
                });
            }
#endif

        }

        private void ReceiveImage(IResourceObject resource)
        {
            Resource = (ImageResource)resource;

#if ANDROID
            Bitmap = Resource.Bitmap;
#elif IOS
            Image = Resource.Image;
#endif
        }

        public new string Source
        {
            get { return source; }
            set { NSource = value; }
        }
    }
}