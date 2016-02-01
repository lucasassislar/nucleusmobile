using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public delegate void SwitchPageDelegate(CustomPage page, CustomPageView current, CustomPageView last, bool returning);
    public delegate void AddPageDelegate(CustomPage page, CustomPageView view);
    public delegate void UpdatePageDelegate(CustomPage page);
}