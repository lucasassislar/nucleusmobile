using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nucleus
{
    public struct Position
    {
        public double X { get; set; }
        public RelativePosition XRel { get; set; }

        public double Y { get; set; }
        public RelativePosition YRel { get; set; }

        public double Width { get; set; }
        public RelativePosition WidthRel { get; set; }

        public double Height { get; set; }
        public RelativePosition HeightRel { get; set; }

        public static double GetValue(double val, RelativePosition rel)
        {
            switch (rel)
            {
                case RelativePosition.None:
                    return val;
                case RelativePosition.ParentWidth:
                    throw new NotImplementedException();
                    break;
                case RelativePosition.ParentHeight:
                    throw new NotImplementedException();
                    break;
                case RelativePosition.ScreenWidth:
                    return Core.PcWidth(val);
                case RelativePosition.ScreenHeight:
                    return Core.PcHeight(val);
                default:
                    throw new NotImplementedException();
            }
        }


        //private Position pos;
        //public double X
        //{
        //    get { return pos.X; }
        //    set { pos.X = value; }
        //}
        //public double Y
        //{
        //    get { return pos.Y; }
        //    set { pos.Y = value; }
        //}
        //public double Width
        //{
        //    get { return pos.Width; }
        //    set { pos.Width = value; }
        //}
        //public double Height
        //{
        //    get { return pos.Height; }
        //    set { pos.Height = value; }
        //}
        //public RelativePosition XRel
        //{
        //    get { return pos.XRel; }
        //    set { pos.XRel = value; }
        //}
        //public RelativePosition YRel
        //{
        //    get { return pos.YRel; }
        //    set { pos.YRel = value; }
        //}
        //public RelativePosition WidthRel
        //{
        //    get { return pos.WidthRel; }
        //    set { pos.WidthRel = value; }
        //}
        //public RelativePosition HeightRel
        //{
        //    get { return pos.HeightRel; }
        //    set { pos.HeightRel = value; }
        //}
    }
}