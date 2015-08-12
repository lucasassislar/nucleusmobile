using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public static class RectUtil
    {
        public static double Center(double length, double areaLength)
        {
            return (areaLength / 2.0) - (length / 2.0);
        }
    }
}