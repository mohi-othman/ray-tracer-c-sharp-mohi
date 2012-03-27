using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

        public System.Drawing.Bitmap ExportImage()
        {
            var picture = new System.Drawing.Bitmap(this.width, this.height);

            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    var r = (int)Pixels[x, y].color.R;
                    var g = (int)Pixels[x, y].color.G;
                    var b = (int)Pixels[x, y].color.B;

                    r = (r > 255) ? 255 : r;
                    g = (g > 255) ? 255 : g;
                    b = (b > 255) ? 255 : b;
                    picture.SetPixel(x, y, System.Drawing.Color.FromArgb(r, g, b));

                }
            }

            return picture;
        }

        public System.Drawing.Bitmap ExportDepthImage()
        {
            var picture = new System.Drawing.Bitmap(this.width, this.height);
            var depthList = new List<double>();

            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    var d = Pixels[x, y].depth;
                    if (d < Globals.infinity)
                        depthList.Add(d);
                }
            }

            var maxDepth = depthList.Max();
            var minDepth = depthList.Min();

            for (int x = 0; x < this.width; x++)
            {
                for (int y = 0; y < this.height; y++)
                {
                    var d = Pixels[x, y].depth;
                    if (d < Globals.infinity)
                    {
                        var c = (int)Math.Ceiling((maxDepth - d) / (maxDepth - minDepth) * 220) + 30;

                        picture.SetPixel(x, y, System.Drawing.Color.FromArgb(c, c, c));
                    }
                    else
                    {
                        picture.SetPixel(x, y, System.Drawing.Color.FromArgb(0, 0, 0));
                    }
                }
            }

            return picture;
        }
    }


}
