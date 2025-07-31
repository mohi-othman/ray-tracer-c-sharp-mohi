using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using SixLabors.ImageSharp.PixelFormats;

namespace RayTracer
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

        public Argb32 Convert()
        {
            return new Argb32((float)R, (float)G, (float)B, 1);
        }

        public static Color operator +(Color a, Color b)
        {
            return new Color(a.R + b.R, a.G + b.G, a.B + b.B);
        }

        public static Color operator -(Color a, Color b)
        {
            return new Color(a.R - b.R, a.G - b.G, a.B - b.B);
        }

        public static Color operator *(Color a, Color b)
        {
            return new Color(a.R * b.R, a.G * b.G, a.B * b.B);
        }

        public static Color operator *(double x, Color C)
        {
            return new Color(C.R * x, C.G * x, C.B * x);
        }

        public static Color operator *(Color C, double x)
        {
            return x * C;
        }
    }
}
