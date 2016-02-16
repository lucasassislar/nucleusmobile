using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class AutoPositionLayout : AbsoluteLayout
    {
        public AutoPositionLayout()
        {

        }

        public void AddView(View view, int width, int height)
        {
            view.WidthRequest = width;
            view.HeightRequest = height;

            this.Children.Add(view);

            //view.SizeChanged += view_SizeChanged;
        }

        private void view_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}