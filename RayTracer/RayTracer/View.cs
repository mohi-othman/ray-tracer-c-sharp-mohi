using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class View
    {
        public Point[,] Pixels { get; set; }
        public int width { get; set; }
        public int height { get; set; }

        public View(int x, int y)
        {
            width = x;
            height = y;
            Pixels = new Point[x, y];
        }

        public struct Point
        {
            public Color color;
            public double depth;
        }
    }


}
