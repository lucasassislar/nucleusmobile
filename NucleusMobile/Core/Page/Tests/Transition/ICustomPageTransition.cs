using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public interface ICustomPageTransition
    {
        void Add(CustomPageView view);
        void Switch(CustomPageView current, CustomPageView last, bool returning);
        void Update();
        void OnTouch(CustomPageView view, TouchData touch);
    }
}