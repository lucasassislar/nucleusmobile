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
using System.Net;

namespace Nucleus
{
    public static class Captcha
    {
        public static void DownloadCaptchaImage(string website, Action<ImageResource, string> onSuccess)
        {
            using (WebClient client = new WebClient())
            {
                ResourceManager resource = Core.Instance.ResourceManager;
                resource.DownloadString(website + "captcha/captcha.ashx",
                    delegate(IResourceObject res)
                    {
                        StringResource str = (StringResource)res;
                        // download the image

                        resource.DownloadImage(website + "captcha/getcaptcha.ashx?CaptchaID=" + str.String,
                            delegate(IResourceObject nres)
                            {
                                if (onSuccess != null)
                                {
                                    onSuccess((ImageResource)nres, str.String);
                                }
                            }, null);
                    }, null);
            }
        }

    }
}