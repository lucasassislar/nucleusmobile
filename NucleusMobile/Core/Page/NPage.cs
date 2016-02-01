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
        private Dictionary<View, Position> positions;

        public NPage()
        {
            positions = new Dictionary<View, Position>();
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

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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
            Pos(view, x, y, width, height);
        }
        public void AddView(View view, double x, RelativePosition xRel,
            double y, RelativePosition yRel,
            double width, RelativePosition widthRel,
            double height, RelativePosition heightRel)
        {
            abs.Children.Add(view);
            Pos(view, x, xRel, y, yRel, width, widthRel, height, heightRel);
        }
        public void AddView(Layout<View> parent, View view, double x, double y, double width, double height)
        {
            parent.Children.Add(view);
            Pos(view, x, y, width, height);
        }
        public void AddView(Layout<View> parent, View view, double x, RelativePosition xRel,
            double y, RelativePosition yRel,
            double width, RelativePosition widthRel,
            double height, RelativePosition heightRel)
        {
            parent.Children.Add(view);
            Pos(view, x, xRel, y, yRel, width, widthRel, height, heightRel);
        }

        public void Pos(View view, double x, double y, double width, double height)
        {
            AbsoluteLayout.SetLayoutBounds(view, new Rectangle(x, y, width, height));
        }

        public void Pos(View view,
            double x, RelativePosition xRel,
            double y, RelativePosition yRel,
            double width, RelativePosition widthRel,
            double height, RelativePosition heightRel)
        {
            Position p = new Position(x, xRel, y, yRel, width, widthRel, height, heightRel);
            Rectangle r = p.GetRectangle();
            //AbsoluteLayout.SetLayoutBounds((View)view, r);

            if (view is INukeView)
            {
                INukeView nuke = (INukeView)view;
                nuke.Position = p;
            }
            else
            {
                if (positions.ContainsKey(view))
                {
                    positions[view] = p;
                }
                else
                {
                    positions.Add(view, p);
                }

                if (view is Layout<View>)
                {
                    view.SizeChanged += view_SizeChanged;
                }
            }
        }

        private void view_SizeChanged(object sender, EventArgs e)
        {
            Layout<View> view = (Layout<View>)sender;
            UpdateLayout(view);
        }

        private void UpdateLayout(Layout<View> layout)
        {
            for (int i = 0; i < layout.Children.Count; i++)
            {
                View v = layout.Children[i];
                if (v is INukeView)
                {
                    INukeView nuke = (INukeView)v;
                    PositionElement(v, nuke);
                }
                else
                {
                    Position p;
                    if (positions.TryGetValue(v, out p))
                    {
                        PositionElement(v, p);
                    }
                }

                if (v is Layout<View>)
                {
                    Layout<View> l = (Layout<View>)v;
                    UpdateLayout(l);
                }
            }
        }

        /// <summary>
        /// Method for loading stuff on demand
        /// </summary>
        public virtual void LoadContent()
        {
        }

        public virtual void PositionElements()
        {
            PositionElements(this.Width, this.Height);
        }
        public virtual void PositionElements(double width, double height)
        {
            foreach (var pos in positions)
            {
                View v = pos.Key;
                Position p = pos.Value;
                PositionElement(v, p);
            }
        }

        private void PositionElement(View v, Position p)
        {
            Rectangle r = p.GetRectangle();
            //v.WidthRequest = r.Width;
            //v.HeightRequest = r.Height;
            //v.X = p.X;
            //v.Y = p.Y;

            if (v is Layout<View>)
            {
                UpdateLayout((Layout<View>)v);
            }
        }
        private void PositionElement(View v, INukeView p)
        {
            //AbsoluteLayout.SetLayoutBounds(v,
            //        new Rectangle(
            //            Position.GetValue(p.X, p.XRel),
            //            Position.GetValue(p.Y, p.YRel),
            //            Position.GetValue(p.Width, p.WidthRel),
            //            Position.GetValue(p.Height, p.HeightRel)));

            if (v is Layout<View>)
            {
                UpdateLayout((Layout<View>)v);
            }
        }
    }
}