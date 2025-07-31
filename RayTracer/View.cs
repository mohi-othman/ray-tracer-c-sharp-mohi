using System;
using System.Collections.Generic;
using System.Linq;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace RayTracer
{
    public class View
    {
        public Point[,] Pixels { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        //Constructor
        public View(int width, int height, Vector3D topLeftCorner, Vector3D topRightCorner, Vector3D bottomLeftCorner, Vector3D bottomRightCorner)
        {
            Width = width;
            Height = height;

            var distanceX = Vector3D.Distance(topRightCorner, topLeftCorner);
            var distanceY = Vector3D.Distance(bottomLeftCorner, topLeftCorner);
            var deltaX = distanceX / width;
            var deltaY = distanceY / height;
            var vectorX = (topRightCorner - topLeftCorner).Normalize();
            var vectorY = (bottomLeftCorner - topLeftCorner).Normalize();

            Pixels = new Point[width, height];

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    var _realCoordinate = new Vector3D(topLeftCorner.x, topLeftCorner.y, topLeftCorner.z);
                    _realCoordinate += vectorX * (deltaX * x);
                    _realCoordinate += vectorY * (deltaY * y);
                    Pixels[x, y] = new Point { color = new Color(), depth = Globals.infinity, realCoordinate = _realCoordinate };
                }

            }
        }

        public struct Point
        {
            public Color color;
            public double depth;
            public Vector3D realCoordinate;
        }

        public Image<Argb32> ExportImage()
        {
            var picture = new Image<Argb32>(Width, Height);

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {                    
                    picture[x, y] = Pixels[x, y].color.Convert();
                }
            }

            return picture;
        }

        public Image<Argb32> ExportDepthImage()
        {
            var picture = new Image<Argb32>(Width, Height);
            var depthList = new List<double>();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var d = Pixels[x, y].depth;
                    if (d < Globals.infinity)
                        depthList.Add(d);
                }
            }

            var maxDepth = 140;
            var minDepth = depthList.Min();

            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    var d = Pixels[x, y].depth;
                    if (d < Globals.infinity)
                    {
                        var c = (int)Math.Ceiling((maxDepth - d) / (maxDepth - minDepth) * 220) + 30;

                        if (c < 0)
                            c = 0;
                        picture[x, y] = new Argb32(c, c, c);
                    }
                    else
                    {
                        picture[x, y] = new Argb32(0, 0, 0);
                    }
                }
            }

            return picture;
        }
    }


}
