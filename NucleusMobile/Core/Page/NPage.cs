using Nucleus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class NPage : ContentPage
    {
        private AbsoluteLayout abs;
        private AbsoluteLayout background;
        private bool loaded;
        private DeviceOrientation lastOrientation = DeviceOrientation.None;

        public NPage()
        {
            abs = new AbsoluteLayout();
            background = new AbsoluteLayout();
            abs.Children.Add(background);
            PlatformManager manager = Core.Instance.PlatformManager;
            AbsoluteLayout.SetLayoutBounds(background,
                new Rectangle(0, 0, manager.GetScreenWidth(), manager.GetScreenHeight()));

            this.Content = abs;
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (!loaded)
            {
                LoadContent();
            }
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            DeviceOrientation ori = (width > height) ? DeviceOrientation.Landscape : DeviceOrientation.Portrait;
            if (lastOrientation != ori)
            {
                lastOrientation = ori;
                PositionElements(width, height);
            }
        }

        public void AddView(View view, double x, double y, double width, double height)
        {
            abs.Children.Add(view);
            AbsoluteLayout.SetLayoutBounds(view,
                new Rectangle(x, y, width, height));
        }
        public void Pos(View view, double x, double y, double width, double height)
        {
            AbsoluteLayout.SetLayoutBounds(view,
             new Rectangle(x, y, width, height));
        }

        /// <summary>
        /// Method for loading stuff on demand
        /// </summary>
        public virtual void LoadContent()
        {
        }

        public virtual void PositionElements(double width, double height)
        {

        }
    }
}