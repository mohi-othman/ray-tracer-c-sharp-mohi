using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Picture
    {
        public Color[,] Pixels { get; set; }

        public Picture(int width, int height)
        {
            Pixels = new Color[width, height];
        }

    }
}
