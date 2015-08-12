using Nucleus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class CustomPage : ContentPage
    {
        private AbsoluteLayout abs;
        private AbsoluteLayout background;
        private List<CustomPageView> views;
        private CustomPageView currentView;
        private CustomPageView lastView;

        public AbsoluteLayout Background
        {
            get { return background; }
        }

        public List<CustomPageView> Views
        {
            get { return views; }
        }

        public CustomPageView CurrentView
        {
            get { return currentView; }
        }

        private ICustomPageTransition transition;
        public ICustomPageTransition Transition
        {
            get { return transition; }
            set { transition = value; }
        }

        public CustomPage()
        {
            double width = Core.Instance.PlatformManager.GetScreenWidth();
            double height = Core.Instance.PlatformManager.GetScreenHeight();

            transition = new TransitionSlideLeft(this);

            NavigationPage.SetHasNavigationBar(this, false);
            NavigationPage.SetHasBackButton(this, false);

            abs = new AbsoluteLayout();

            this.Content = abs;

            background = new AbsoluteLayout();
            abs.Children.Add(background);
           
            AbsoluteLayout.SetLayoutBounds(background, new Rectangle(0, 0, width, height));

            views = new List<CustomPageView>();
        }
        public CustomPage(CustomPageView view)
            : this()
        {
            AddView(view, 0);
        }

        public void AddView(CustomPageView view, int x)
        {
            if (currentView == null)
            {
                currentView = view;
            }

            views.Add(view);
            abs.Children.Add(view);

            if (transition != null)
            {
                transition.Add(view);
            }

            view.SetParent(this);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            Core.Instance.OnUpdate -= Update;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Core.Instance.OnUpdate += Update;
        }

        public void SwitchCurrent(int view)
        {
            if (view >= views.Count)
            {
                return;
            }

            SwitchCurrent(views[view], false);
        }

        public void SwitchCurrent(CustomPageView view, bool returning)
        {
            lastView = currentView;
            currentView = view;

            if (transition != null)
            {
                transition.Switch(currentView, lastView, returning);
            }
        }

        private void Update()
        {
            if (currentView != null)
            {
                //currentView.Tick();
            }

            Core.Instance.PlatformManager.RunOnUIThread(delegate
            {
                if (transition != null)
                {
                    transition.Update();
                }
            });
        }

        protected override bool OnBackButtonPressed()
        {
            if (this.lastView != null)
            {
                SwitchCurrent(lastView, true);
                lastView = null;
            }
            return true;
        }

        public void OnTouch(CustomPageView page, TouchData data)
        {
            if (transition != null)
            {
                transition.OnTouch(page, data);
            }
        }
    }
}
