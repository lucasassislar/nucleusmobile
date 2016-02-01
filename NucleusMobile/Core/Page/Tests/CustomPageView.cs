using Nucleus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public class CustomPageView : AbsoluteLayout
    {
        public object TransData;
        private CustomPage parent;

        public CustomPageView()
        {
            this.BackgroundColor = Color.Transparent;
        }

        public CustomPageView(CustomPage parent)
            : this()
        {
            this.parent = parent;
        }

        public void SetParent(CustomPage par)
        {
            parent = par;
        }

        public void OnTouch(TouchData data)
        {
            if (parent != null)
            {
                parent.OnTouch(this, data);
            }
        }
    }
}