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

                if (!source.Contains(@"//")) // scheme
                {
                    source = Uri.UriSchemeFile + Uri.SchemeDelimiter + source;
                }

                Uri uri = new Uri(source);
                if (uri.Scheme == Uri.UriSchemeFile)
                {
                    Core.Instance.ResourceManager.LoadFile(source, ResourceType.Image, ReceiveImage, null, true);
                }
                else
                {
                    Core.Instance.ResourceManager.DownloadImage(uri, ReceiveImage, null);
                }
            }
        }

        private Position pos;
        public double X
        {
            get { return pos.X; }
            set { pos.X = value; }
        }
        public double Y
        {
            get { return pos.Y; }
            set { pos.Y = value; }
        }
        public double Width
        {
            get { return pos.Width; }
            set { pos.Width = value; }
        }
        public double Height
        {
            get { return pos.Height; }
            set { pos.Height = value; }
        }
        public RelativePosition XRel
        {
            get { return pos.XRel; }
            set { pos.XRel = value; }
        }
        public RelativePosition YRel
        {
            get { return pos.YRel; }
            set { pos.YRel = value; }
        }
        public RelativePosition WidthRel
        {
            get { return pos.WidthRel; }
            set { pos.WidthRel = value; }
        }
        public RelativePosition HeightRel
        {
            get { return pos.HeightRel; }
            set { pos.HeightRel = value; }
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
            if (imgView != null && Bitmap != null && !isDisposed)
            {
                Core.Instance.PlatformManager.RunOnUIThread(delegate
                {
                    imgView.SetImageBitmap(Bitmap);
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

        private void ReceiveImage(IResourceObject resource)
        {
            Resource = (ImageResource)resource;

#if ANDROID
            Bitmap = Resource.Bitmap;
#elif IOS
            Image = Resource.Image;
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