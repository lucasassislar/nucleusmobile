using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class AutoPositionLayout : AbsoluteLayout
    {
        public StackOrientation Orientation { get; set; }

        public void AddView(View view)
        {
            this.Children.Add(view);
            view.SizeChanged += view_SizeChanged;
        }

        private void view_SizeChanged(object sender, EventArgs e)
        {

        }
    }
}