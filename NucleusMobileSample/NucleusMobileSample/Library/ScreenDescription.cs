using System;
using System.Collections.Generic;
using System.Text;

namespace NucleusMobileSample
{
    public struct ScreenDescription
    {
        public string name;
        public Type type;

        public ScreenDescription(string n, Type t)
        {
            name = n;
            type = t;
        }

        public override string ToString()
        {
            return name;
        }
    }
}
