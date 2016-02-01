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
    public class CustomImage : Image, INukeView
    {
        public ImageResource Resource;

#if ANDROID
        private ImageView imgView;

        public ImageView ImgView
        {
            get { return imgView; }
            set
            {
                imgView = value;
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
#endif

        private string source;
        public string NSource
        {
            get { return source; }
            set
            {
                source = value;

                if (!source.Contains(@"//")) // scheme
                {
                    source = Uri.UriSchemeFile + Uri.SchemeDelimiter + source;
                }

                Uri uri = new Uri(source);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    Core.Instance.ResourceManager.LoadFile<ImageResource>(source, ResourceType.Image, ReceiveImage, null, true);
                }
                else
                {
                    Core.Instance.ResourceManager.DownloadImage(uri, ReceiveImage, null);
                }
            }
        }

        private Position pos;
        public Position Position
        {
            get { return pos; }
            set { pos = value; }
        }

        public CustomImage()
        {
#if IOS
            base.Source = "pixel.png";
#endif
        }

        private void UpdateImgView()
        {
            if (Resource == null)
            {
                return;
            }

            bool isDisposed = Resource.IsDisposed();
#if ANDROID
            if (imgView != null && !isDisposed)
            {
                Core.Instance.PlatformManager.RunOnUIThread(delegate
                {
                    // update size
                    Bitmap bitmap = Resource.Bitmap;
                    Rectangle r = pos.GetRectangle(bitmap.Width, bitmap.Height);
                    this.WidthRequest = r.Width;
                    this.HeightRequest = r.Height;

                    imgView.SetImageBitmap(Resource.Bitmap);
                });
            }
#elif IOS
            if (imgView != null && image != null && !isDisposed)
            {
                Core.Instance.PlatformManager.RunOnUIThread(delegate
                {
                    imgView.Image = image;
                });
            }
#endif

        }

        public event Action<CustomImage> ReceivedImage;

        private void ReceiveImage(ImageResource resource)
        {
            Resource = resource;
#if ANDROID
            UpdateImgView();
#endif

            if (ReceivedImage != null)
            {
                ReceivedImage(this);
            }

            Element el = this.Parent;
            while (el != null)
            {
                if (el is NPage)
                {
                    NPage page = (NPage)el;
                    page.PositionElements();
                }
                el = el.Parent;
            }
        }

        public new string Source
        {
            get { return source; }
            set { NSource = value; }
        }
    }
}