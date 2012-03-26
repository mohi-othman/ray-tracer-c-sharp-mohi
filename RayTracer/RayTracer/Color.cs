using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace RayTracer.RayTracer
{
    public class Color
    {
        public double R { get; set; }
        public double G { get; set; }
        public double B { get; set; }

        public Color()
        {
            R = 0;
            G = 0;
            B = 0;
        }

        public Color(double Red, double Green, double Blue)
        {
            R = Red;
            B = Blue;
            G = Green;
        }

        public System.Drawing.Color Convert()
        {
            return System.Drawing.Color.FromArgb((int)R, (int)G, (int)B);
        }
    }
}
