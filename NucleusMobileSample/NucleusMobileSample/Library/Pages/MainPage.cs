using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Nucleus;

#if IOS
using Foundation;
using UIKit;
#endif

namespace NucleusMobileSample
{
    public class MainPage : NPage
    {
        private ListView list;

        public MainPage()
        {
            list = new ListView();
            list.ItemTapped += list_ItemTapped;
            AddView(list, 0, 0, Core.PcWidth(1), Core.PcHeight(1));

            List<ScreenDescription> cells = new List<ScreenDescription>();
            cells.Add(new ScreenDescription("On Demand Loading", typeof(OnDemandPage)));
            list.ItemsSource = cells;

            this.Title = "Samples";
        }

        void list_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            ScreenDescription scr = (ScreenDescription)e.Item;
            NPage page = (NPage)Activator.CreateInstance(scr.type);
            Navigation.PushAsync(page);
        }

        public override void PositionElements(double width, double height)
        {
            base.PositionElements(width, height);

            Pos(list, 0, 0, width, height);
        }
    }
}