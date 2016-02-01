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

        private Label header;
        private Label bottom;

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

            header = new Label();
            header.Text = "Header";
            AddView(header, 0, RelativePosition.None,
                            0, RelativePosition.None,
                            1, RelativePosition.ScreenWidth,
                            10, RelativePosition.ScreenHeight);
            
            for (int i = 0; i < images.Length; i++)
            {
                string img = images[i];

                CustomImage image = new CustomImage();
                image.Source = img;

                stack.Children.Add(image);
                AddView(stack, image,
                                   0, RelativePosition.None,
                                   0, RelativePosition.None,
                                   1, RelativePosition.ScreenWidth,
                                   0, RelativePosition.None);
            }

            bottom = new Label();
            bottom.Text = "Bottom";
            AddView(bottom, 0, RelativePosition.None,
                            0, RelativePosition.None,
                            1, RelativePosition.ScreenWidth,
                            30, RelativePosition.None);
        }
    }
}
