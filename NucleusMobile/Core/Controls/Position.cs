using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace Nucleus
{
    public struct Position
    {
        private double x;
        private double y;
        private double width;
        private double height;

        private RelativePosition yRel;
        private RelativePosition xRel;
        private RelativePosition widthRel;
        private RelativePosition heightRel;

        public RelativePosition XRel
        {
            get { return xRel; }
            set { xRel = value; }
        }

        public RelativePosition YRel
        {
            get { return yRel; }
            set { yRel = value; }
        }

        public RelativePosition WidthRel
        {
            get { return widthRel; }
            set { widthRel = value; }
        }
        public RelativePosition HeightRel
        {
            get { return heightRel; }
            set { heightRel = value; }
        }

        public double X
        {
            get { return x; }
            set { x = value; }
        }

        public double Y
        {
            get { return y; }
            set { y = value; }
        }

        public double Width
        {
            get { return width; }
            set { width = value; }
        }

        public double Height
        {
            get { return height; }
            set { height = value; }
        }

       

        public Position(double xx, RelativePosition xxRel, 
            double yy, RelativePosition yyRel, 
            double wwidth, RelativePosition wwidthRel, 
            double hheight, RelativePosition hheightRel)
        {
            x = xx;
            y = yy;
            width = wwidth;
            height = hheight;

            xRel = xxRel;
            yRel = yyRel;
            widthRel = wwidthRel;
            heightRel = hheightRel;
        }

        public static double GetValue(double val, RelativePosition rel)
        {
            switch (rel)
            {
                case RelativePosition.None:
                    return val;
                case RelativePosition.ScreenWidth:
                case RelativePosition.ParentWidth:
                    return Core.PcWidth(val);
                case RelativePosition.ScreenHeight:
                case RelativePosition.ParentHeight:
                    return Core.PcHeight(val);
                default:
                    throw new NotImplementedException();
            }
        }

        public static double GetValue(double val, RelativePosition rel, double parentWidth, double parentHeight)
        {
            switch (rel)
            {
                case RelativePosition.None:
                    return val;
                case RelativePosition.ScreenWidth:
                    return Core.PcWidth(val);
                case RelativePosition.ScreenHeight:
                    return Core.PcHeight(val);
                case RelativePosition.ParentWidth:
                    return val * parentWidth;
                case RelativePosition.ParentHeight:
                    return val * parentHeight;
                default:
                    throw new NotImplementedException();
            }
        }


        public Rectangle GetRectangle()
        {
            Rectangle r = new Rectangle();
            r.X = GetValue(X, XRel);
            r.Y = GetValue(Y, YRel);
            r.Width = GetValue(Width, WidthRel);
            r.Height = GetValue(Height, HeightRel);
            return r;
        }

        public Rectangle GetRectangle(double width, double height)
        {
            Rectangle r = new Rectangle();
            r.X = GetValue(X, XRel, width, height);
            r.Y = GetValue(Y, YRel, width, height);

            if (WidthRel == RelativePosition.None &&
                HeightRel != RelativePosition.None)
            {

            }
            else if (WidthRel != RelativePosition.None &&
                HeightRel == RelativePosition.None)
            {
                // height is relative to width
                r.Width = GetValue(Width, WidthRel, width, height);
                r.Height = (height / width) * r.Width;
            }

            return r;
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