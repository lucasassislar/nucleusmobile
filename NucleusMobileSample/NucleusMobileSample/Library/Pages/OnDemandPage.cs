using Nucleus;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace NucleusMobileSample
{
    public class OnDemandPage : NPage
    {
        private string[] images = new string[]
        {
            "https://pbs.twimg.com/profile_images/471641515756769282/RDXWoY7W.png",
            "http://comocriaraplicativos.com.br/wp-content/uploads/2015/02/Xamarin-Inc..zpoh_xamarin-logo-hexagon-blue.png",
            "https://components.xamarin.com/resources/icons/component-1854/screenshot1.jpg",
            "https://blog.xamarin.com/wp-content/uploads/2012/02/graph1.png",
            "https://techblog.betclicgroup.com/wp-content/uploads/2014/03/xamarinplatforms.jpg",
            "https://components.xamarin.com/resources/icons/component-1885/XamarinDashboard.png",
            "http://winsupersite.com/site-files/winsupersite.com/files/imagecache/large_img/uploads/2013/11/xamarin-hero.jpg",
            "http://www.redbitdev.com/wp-content/uploads/2014/04/xamarin.jpeg",
            "https://blog.xamarin.com/wp-content/uploads/2015/05/Meet-Xamarin.Forms_4.png",
            "http://www.misfitgeek.com/wp-content/uploads/2014/06/Xamarin_Forms-300x171.png"
        };

        private ScrollView scroll;
        private StackLayout stack;

        public OnDemandPage()
        {
            scroll = new ScrollView();
            AddView(scroll, 0, RelativePosition.None, 0, RelativePosition.None, 1, RelativePosition.ScreenWidth, 1, RelativePosition.ScreenHeight);

            stack = new StackLayout();
            stack.Orientation = StackOrientation.Vertical;
            scroll.Content = stack;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            for (int i = 0; i < images.Length; i++)
            {
                string img = images[i];
                img = "print.png";

                CustomImage image = new CustomImage();
                image.Source = img;
                //image.ReceivedImage += delegate
                //{
                //    Core.Instance.PlatformManager.RunOnUIThread(delegate ()
                //    {
                        
                //    });
                //};

                AddView(stack, image,
                                   0, RelativePosition.None,
                                   0, RelativePosition.None,
                                   1, RelativePosition.ScreenWidth,
                                   1, RelativePosition.None);
            }


        }
    }
}
