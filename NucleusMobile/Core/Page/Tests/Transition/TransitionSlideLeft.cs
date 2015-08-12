using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class TransitionSlideLeft : ICustomPageTransition
    {
        private CustomPage page;
        public TransitionSlideLeft(CustomPage page)
        {
            this.page = page;
        }

        public void Add(CustomPageView view)
        {
            if (currentView == null)
            {
                currentView = view;
            }

            double width = Core.Instance.PlatformManager.GetScreenWidth();
            double height = Core.Instance.PlatformManager.GetScreenHeight();

            TransitionSlideData data = new TransitionSlideData();
            if (view != page.CurrentView)
            {
                data.CurrentPosition = width;
                data.TargetPosition = width;
            }
            data.Speed = 0.2;
            view.TransData = data;

            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(data.CurrentPosition, 0, width, height));
        }

        private CustomPageView currentView;
        private CustomPageView lastView;

        public void Switch(CustomPageView current, CustomPageView last, bool returning)
        {
            this.currentView = current;
            this.lastView = last;

            double width = Core.Instance.PlatformManager.GetScreenWidth();

            List<CustomPageView> views = page.Views;
            for (int i = 0; i < views.Count; i++)
            {
                CustomPageView v = views[i];
                v.IsEnabled = false;
                v.IsVisible = false; // disable everything
            }

            current.IsEnabled = true;
            current.IsVisible = true;
            TransitionSlideData curData = (TransitionSlideData)current.TransData;
            curData.TargetPosition = 0;

            if (last != null)
            {
                last.IsEnabled = true;
                last.IsVisible = true;
                TransitionSlideData lastData = (TransitionSlideData)last.TransData;

                if (returning)
                {
                    lastData.TargetPosition = width;
                }
                else
                {
                    lastData.TargetPosition = -width;
                }
            }
        }

        public void Update()
        {
            double width = Core.Instance.PlatformManager.GetScreenWidth();
            double height = Core.Instance.PlatformManager.GetScreenHeight();

            List<CustomPageView> views = page.Views;
            for (int i = 0; i < views.Count; i++)
            {
                CustomPageView v = views[i];
                TransitionSlideData data = (TransitionSlideData)v.TransData;

                if (v.IsEnabled)
                {
                    data.CurrentPosition = DoubleUtil.Lerp(data.CurrentPosition, data.TargetPosition, data.Speed);
                    AbsoluteLayout.SetLayoutBounds(v, new Rectangle(data.CurrentPosition, 0, width, height));
                }
            }
        }

        public void OnTouch(CustomPageView view, TouchData touch)
        {
        }
    }
}