using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public interface INukeView
    {
        double X { get; set; }
        RelativePosition XRel { get; set; }

        double Y { get; set; }
        RelativePosition YRel { get; set; }

        double Width { get; set; }
        RelativePosition WidthRel { get; set; }

        double Height { get; set; }
        RelativePosition HeightRel { get; set; }
    }
}