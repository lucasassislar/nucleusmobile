using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public struct TouchData
    {
        public double x;
        public double y;
        public TouchState state;

#if ANDROID
        public static TouchData FromAndroid(Android.Views.MotionEvent e)
        {
            TouchData t = new TouchData();

            switch (e.Action)
            {
                case Android.Views.MotionEventActions.Up:
                    t.state = TouchState.Up;
                    break;
                case Android.Views.MotionEventActions.Down:
                    t.state = TouchState.Down;
                    break;
                case Android.Views.MotionEventActions.Move:
                    t.state = TouchState.Move;
                    break;
                default:
                    break;
            }

            t.x = e.GetX();
            t.y = e.GetY();

            return t;

        }
#endif
    }
}