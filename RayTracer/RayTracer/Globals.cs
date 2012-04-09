using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RayTracer.RayTracer
{
    public class Globals
    {
        public static double infinity = double.PositiveInfinity;
        public static double epsilon = 0.0001f;
        public static int maxDepth = 6;
        
        public static double Max(double a, double b)
        {
            return a > b ? a : b;
        }

        public static double Min(double a, double b)
        {
            return a < b ? a : b;
        }
    }
}
