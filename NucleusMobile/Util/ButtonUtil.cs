using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;


namespace Nucleus
{
    public static class ButtonUtil
    {
        private static Stopwatch stop;
        public static bool CanClick()
        {
            if (stop == null)
            {
                stop = Stopwatch.StartNew();
            }
            else
            {
                if (stop.ElapsedMilliseconds < 500)
                {
                    return false;
                }
                stop.Restart();
            }
            return true;
        }
    }
}