using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Nucleus
{
    public static class Captcha
    {
        public static void DownloadCaptchaImage(string website, Action<ImageResource, string> onSuccess, Action onFail)
        {
            //#if WP8
            //            using (WebClientWP8 client = new WebClientWP8())
            //#else
            //            using (WebClient client = new WebClient())
            //#endif
            //            {
            //                ResourceManager resource = Core.Instance.ResourceManager;
            //                resource.DownloadString(new Uri(website + "captcha/captcha.ashx"),
            //                    delegate(IResourceObject res)
            //                    {
            //                        StringResource str = (StringResource)res;
            //                        // download the image

            //                        resource.DownloadImage(new Uri(website + "captcha/getcaptcha.ashx?CaptchaID=" + str.String),
            //                            delegate(IResourceObject nres)
            //                            {
            //                                if (onSuccess != null)
            //                                {
            //                                    onSuccess((ImageResource)nres, str.String);
            //                                }
            //                            }, onFail, null);
            //                    }, onFail, null);
            //            }
        }

    }
}