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
            var r = (int)Math.Ceiling(R * 255);
            var g = (int)Math.Ceiling(G * 255);
            var b = (int)Math.Ceiling(B * 255);

            if (r > 255)
                r = 255;
            if (g > 255)
                g = 255;
            if (b > 255)
                b = 255;

            if (r < 0)
                r = 0;
            if (g < 0)
                g = 0;
            if (b < 0)
                b = 0;

            return System.Drawing.Color.FromArgb(r, g, b);
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
